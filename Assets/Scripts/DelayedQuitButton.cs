using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class DelayedQuitButton : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler
{
  [SerializeField]
  private float m_quitDelay = 3f;
  private Coroutine m_quitCoroutine;

  public void OnPointerDown(PointerEventData eventData)
  {
    this.m_quitCoroutine = this.StartCoroutine(this.QuitCoroutine());
  }

  public void OnPointerUp(PointerEventData eventData) => this.StopCoroutine(this.m_quitCoroutine);

  private IEnumerator QuitCoroutine()
  {
    yield return (object) new WaitForSeconds(this.m_quitDelay);
    Debug.Log((object) "Quitting the application!");
    Application.Quit();
  }
}
