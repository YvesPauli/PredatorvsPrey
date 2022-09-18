using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    private int inputSize, outputSize;
    private int[] hiddenSizes;

    private List<float[,]> weights;

    private List<int> layerSizes;

    public Brain(int inputSize, int[] hiddenSizes, int outputSize)
    {
        this.inputSize = inputSize;
        this.outputSize = outputSize;
        this.hiddenSizes = hiddenSizes;

        layerSizes = new List<int>();
        layerSizes.Add(inputSize);
        layerSizes.AddRange(hiddenSizes);
        layerSizes.Add(outputSize);

        weights = new List<float[,]>();
        for (int i = 0; i < layerSizes.Count - 1; i++)
        {
            float[,] weightMatrix = new float[layerSizes[i], layerSizes[i + 1]];

            RandomizeWeights(weightMatrix);

            weights.Add(weightMatrix);
        }
    }

    private void RandomizeWeights(float[,] weightMatrix)
    {
        for (int i = 0; i < weightMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < weightMatrix.GetLength(1); j++)
            {
                weightMatrix[i, j] = Random.Range(-1f, 1f);
            }
        }
    }

    private float[,] WeightEvolution(float[,] weightMatrix)
    {
        float[,] evolvedWeights = new float[weightMatrix.GetLength(0), weightMatrix.GetLength(1)];
        for (int i = 0; i < weightMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < weightMatrix.GetLength(1); j++)
            {   
                evolvedWeights[i, j] = weightMatrix[i,j] + Random.Range(-0.15f, 0.15f);
                evolvedWeights[i,j] = Mathf.Clamp(evolvedWeights[i,j],-1,1);
            }
        }
        return evolvedWeights;
    }

    public void EvolveFromParent(Brain parentBrain)
    {
        weights.Clear();

        foreach(var weightMatrix in parentBrain.weights)
        {
            weights.Add(WeightEvolution(weightMatrix));
        }
    }


    public float[] Process(float[] input)
    {
        Debug.Assert(input.Length == inputSize);

        int depth = hiddenSizes.Length;

        float[] prevValues = input;
        for (int layer = 1; layer < depth + 1; layer++)
        {
            float[] newValues = new float[layerSizes[layer]];
            for (int curr_ind = 0; curr_ind < layerSizes[layer]; curr_ind++)
            {
                float neuronValue = 0;
                for (int prev_ind = 0; prev_ind < layerSizes[layer - 1]; prev_ind++)
                {
                    neuronValue += weights[layer - 1][prev_ind, curr_ind] * prevValues[prev_ind];
                }
                neuronValue = ActivationFunctionReLu(neuronValue);

                newValues[curr_ind] = neuronValue;
            }
            prevValues = newValues;
        }

        // last layer
        float[] output = new float[outputSize];
        for (int curr_ind = 0; curr_ind < outputSize; curr_ind++)
        {
            float neuronValue = 0;
            for (int prev_ind = 0; prev_ind < layerSizes[depth]; prev_ind++)
            {
                neuronValue += weights[depth][prev_ind, curr_ind] * prevValues[prev_ind];
            }

            // last layer has no activation function
            output[curr_ind] = neuronValue;
        }

        return output;
    }

    public float ActivationFunctionReLu(float input)
    {
        if (input < 0) return 0;
        return input;
    }

    public float ActivationFunctionSigmoid(float input)
    {
        return 1f / (1 + Mathf.Exp(-input));
    }
}
