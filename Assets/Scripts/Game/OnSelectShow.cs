using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectShow : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject objectToShow;

    public void OnSelect(BaseEventData eventData)
    {
        objectToShow.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        objectToShow.SetActive(false);
    }
    void Start()
    {
        objectToShow.SetActive(false);
    }
}
