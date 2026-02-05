using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#nullable disable
public class DelayedResetButton : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler
{
  [SerializeField]
  private float m_quitDelay = 3f;
  [SerializeField]
  private GameObject textDisplay;
  [SerializeField]
  private GameObject startButton;
  private Coroutine m_quitCoroutine;
  private Master master;
  private Items[] items;

  private void Start()
  {
    this.master = UnityEngine.Object.FindAnyObjectByType<Master>();
    this.items = this.master.items;
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    this.m_quitCoroutine = this.StartCoroutine(this.ManualResetPrices());
  }

  public void OnPointerUp(PointerEventData eventData) => this.StopCoroutine(this.m_quitCoroutine);

  public IEnumerator ManualResetPrices()
  {
    DelayedResetButton delayedResetButton = this;
    yield return (object) new WaitForSeconds(delayedResetButton.m_quitDelay);
    foreach (Items items in delayedResetButton.items)
      PlayerPrefs.DeleteKey("Amount" + items.name.ToString());
    PlayerPrefs.SetString("LastChangeDate", DateTime.Now.ToString("yyyy-MM-dd"));
    foreach (Items items in delayedResetButton.items)
      PlayerPrefs.SetInt("Amount" + items.name.ToString(), items.amount);
    delayedResetButton.textDisplay.SetActive(true);
    yield return (object) new WaitForSeconds(0.5f);
    delayedResetButton.textDisplay.SetActive(false);
    yield return (object) new WaitForSeconds(0.1f);
    delayedResetButton.StopAllCoroutines();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
