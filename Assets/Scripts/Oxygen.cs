using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oxygen : MonoBehaviour
{

    [SerializeField]
    private float depletionTime = 200;

    private float _secondsPerPercentage;

    private int _currentOxygen = 100;

    private TextMeshProUGUI _oxygenMeterText;

    private float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _oxygenMeterText = GetComponent<TextMeshProUGUI>();

        _secondsPerPercentage = depletionTime / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _secondsPerPercentage)
        {
            _timer -= _secondsPerPercentage;
            _currentOxygen -= 1;
            _oxygenMeterText.text = _currentOxygen.ToString("") + "%";
            
            if(_currentOxygen <= 0)
            {
                Debug.Log("Out of oxygen, you die!");    
            }
        }
    }
}
