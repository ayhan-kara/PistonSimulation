using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager Instance;
	#endregion

	#region UI References
	[Header("UI")]
	[SerializeField] GameObject scenePanel;
	[SerializeField] GameObject winPanel;
    #endregion

    #region Variables
    public int pistonCount = 0;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (pistonCount == 10)
        {
            scenePanel.SetActive(false);
            winPanel.SetActive(true);
        }
    }
    #endregion
}
