using System;
using System.Collections.Generic;
using UnityEngine;
using Gtec.Chain.Common.Algorithms;
using Gtec.Chain.Common.Templates.Utilities;
using Unity.VisualScripting;

public class AccuracyController : MonoBehaviour
{
    [SerializeField]
    private GameObject AccuracyObject;
    public Material NoClassifier;
    public Material VeryGoodClassifier;
    public Material GoodClassifier;
    public Material BadClassifier;

    private Renderer _renderer;

    private enum ClassifierAccuracyLabel { NA, VeryGood, Good, Bad }

    private bool _update;
    private ClassifierAccuracyLabel _classifierAccuracy;

    void Start()
    {
        _renderer = AccuracyObject.GetComponent<Renderer>();
        _classifierAccuracy = ClassifierAccuracyLabel.NA;
    }

    public void UpdateClassifierAccuracy(CalibrationResult calibrationResult)
    {
        var classifierAccuracy = calibrationResult.CalibrationQuality.ToString();
        switch (classifierAccuracy)
        {
            case "Good":
                {
                    _classifierAccuracy = ClassifierAccuracyLabel.VeryGood;
                    break;
                }
            case "Ok":
                {
                    _classifierAccuracy = ClassifierAccuracyLabel.Good;
                    break;
                }
            case "Bad":
                {
                    _classifierAccuracy = ClassifierAccuracyLabel.Bad;
                    break;
                }
            default:
                {
                    _classifierAccuracy = ClassifierAccuracyLabel.NA;
                    break;
                }
        }
        _update = true;
    }

    void Update()
    {
        if (_update)
        {
            switch (_classifierAccuracy)
            {
                case ClassifierAccuracyLabel.NA:
                    {
                        _renderer.material = NoClassifier;
                        break;
                    }
                case ClassifierAccuracyLabel.VeryGood:
                    {
                        _renderer.material = VeryGoodClassifier;
                        break;
                    }
                case ClassifierAccuracyLabel.Good:
                    {
                        _renderer.material = GoodClassifier;
                        break;
                    }
                case ClassifierAccuracyLabel.Bad:
                    {
                        _renderer.material = BadClassifier;
                        break;
                    }
            }
            _update = false;
        }
    }
}