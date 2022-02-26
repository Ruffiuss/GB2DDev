using UnityEngine;
using UnityEngine.UI;

public class PopUpNoticeView : MonoBehaviour
{
    #region Fields

    public Text Text;
    public Image Image;
    public RectTransform Container;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        Image = GetComponent<Image>();
        Container = GetComponent<RectTransform>();
    }

    #endregion

    #region Methods

    public void Init()
    {
        gameObject.SetActive(true);
    }

    #endregion
}