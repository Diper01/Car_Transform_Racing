using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEntities : MonoBehaviour, IButtonEntitiesSelecting
{

    private Button button;
    //private Animator animator;

    void Awake()
    {
        button = GetComponent<Button>();
    }
    public void OnClick()
    {
        button.onClick.Invoke();
    }

    public void Select()
    {
        button.Select();
    }
    public void Deselect()
    {
        transform.localScale = Vector3.one;
        button.OnSelect(null);
    }
    public bool IsSelectble()
    {
        return transform.parent.gameObject.activeInHierarchy && gameObject.activeInHierarchy && button.interactable;
    }
}
