using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
  [HideInInspector]
  public Items[] items;
  public Animator animator;
  public GameObject win;
  public TextMeshProUGUI priceText;
  private List<Items> copyItems;

  [SerializeField] private GameObject textContainer;

  private void Start()
  {
    AssignItems();
    AssignNames();
    Reset();
  }

  private void AssignItems()
  {
    string pathToFile = Path.Combine(Application.streamingAssetsPath, "prizes.json");
    string json = File.ReadAllText(pathToFile);

    ItemWrapper itemWrapper = JsonUtility.FromJson<ItemWrapper>(json);

    if (itemWrapper == null || itemWrapper.items == null)
    {
        throw new Exception("JSON parse failed");
    }

    this.items = itemWrapper.items;
  }

  private void AssignNames()
  {
    foreach (Items item in this.items)
    {
      foreach (Transform child in this.textContainer.transform)
      {
        if (child.name == item.ID.ToString())
        {
          child.GetComponent<TextMeshProUGUI>().text = item.name;
          break;
        }
      }
    }
  }

  public void Reset()
  {
    this.copyItems = new List<Items>();

    foreach (Items items in this.items)
    {
      if (items.amount > 0)
        this.copyItems.Add(new Items()
        {
          name = items.name,
          ID = items.ID,
          amount = items.amount,
          weight = items.weight
        });
    }
    Debug.Log((object) "Reseting price values...");
  } 

  public void OnButtonPressed() => this.StartCoroutine(this.DoAnim());

  private IEnumerator DoAnim()
  {
    FindAnyObjectByType<Button>().gameObject.SetActive(false);

    int maxExclusive = 0;
    foreach (Items copyItem in this.copyItems)
    {
      maxExclusive += (int) copyItem.weight;
    }
    int randomNum = UnityEngine.Random.Range(0, maxExclusive);
    
    int sumWeights = 0;
    int winningIndex = this.copyItems.Count - 1;

    for (int i = 0; i < this.copyItems.Count; ++i)
    {
      if (sumWeights + (int) this.copyItems[i].weight > randomNum)
      {
        winningIndex = i;
        break;
      }
      sumWeights += (int) this.copyItems[i].weight;
    }
    Items currentItem = this.copyItems[winningIndex];
    --currentItem.amount;
    this.copyItems[winningIndex] = currentItem;

    this.animator.SetInteger("AnimToPlay", currentItem.ID);
    yield return new WaitForSeconds(3.5f);

    if (currentItem.amount <= 0)
    {
      Debug.Log("No more of " + currentItem.name);
      this.copyItems.Remove(currentItem);
    }

    this.priceText.text = currentItem.name;
    this.win.SetActive(true);
    StartCoroutine(Helpers.WaitAndExecute(3.1f, () => ResetPos()));
  }

  public void ResetPos()
  {
    FindAnyObjectByType<Button>(FindObjectsInactive.Include).gameObject.SetActive(true);
    this.animator.SetInteger("AnimToPlay", 0);
    this.animator.gameObject.transform.localEulerAngles = Vector3.zero;
  }
}
