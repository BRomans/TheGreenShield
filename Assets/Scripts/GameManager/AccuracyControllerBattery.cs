using System;
using System.Collections.Generic;
using UnityEngine;
using Gtec.Chain.Common.Templates.Utilities;
using UnityEngine.UI;

public class AccuracyControllerBattery : MonoBehaviour
{
    [SerializeField]
    private GameObject NoBattery;

    [SerializeField]
    private GameObject LowBattery;

    [SerializeField]
    private GameObject FullBattery;

    [SerializeField]
    private float blinkSpeed = 1.0f;

    private GameObject _battery;
    

    private enum ClassifierAccuracyLabel { NA, VeryGood, Good, Bad }

    private bool _update;
    private ClassifierAccuracyLabel _classifierAccuracy;

    void Start()
    {
        _battery = NoBattery;
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
        BlinkBattery(_battery);
        if (_update)
        {
            switch (_classifierAccuracy)
            {
                case ClassifierAccuracyLabel.NA:
                    {
                        NoBattery.SetActive(true);
                        LowBattery.SetActive(false);
                        FullBattery.SetActive(false);
                        _battery = NoBattery;
                        break;
                    }
                case ClassifierAccuracyLabel.VeryGood:
                    {
                        NoBattery.SetActive(false);
                        LowBattery.SetActive(false);
                        FullBattery.SetActive(true);
                        _battery = FullBattery;
                        break;
                    }
                case ClassifierAccuracyLabel.Good:
                    {
                        NoBattery.SetActive(false);
                        LowBattery.SetActive(true);
                        FullBattery.SetActive(false);
                        _battery = LowBattery;
                        break;
                    }
                case ClassifierAccuracyLabel.Bad:
                    {
                        NoBattery.SetActive(true);
                        LowBattery.SetActive(false);
                        FullBattery.SetActive(false);

                        break;
                    }
            }
            _update = false;
        }
    }

    private void BlinkBattery(GameObject battery)
    {
        Image image = battery.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time * blinkSpeed, 1));
    }
}