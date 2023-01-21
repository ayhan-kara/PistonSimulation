using UnityEngine;
using UnityEngine.UI;

public class RotatePistonManager : MonoBehaviour
{
    #region Slider References
    [Header ("Slider")]
	[SerializeField] Slider xAxisSlider;
    #endregion

    #region Environment
    float xValue;
    [Header ("References")]
    [SerializeField] Transform piston;
    [SerializeField] float xLimit;
    #endregion


    #region Monobehaviour
    private void Start()
    {
        xAxisSlider.onValueChanged.AddListener(delegate { RotatePistonAxisX(); });
    }
    private void Update()
    {
        xValue = xAxisSlider.value * xLimit;
    }
    #endregion

    #region Rotate Functions
    void RotatePistonAxisX()
    {
        piston.eulerAngles = new Vector3(0, xAxisSlider.value * xLimit);
    }
    #endregion
}
