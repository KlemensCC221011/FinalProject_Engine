using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CollectionBar : MonoBehaviour
{
    [SerializeField] private GameObject collectionBar;
    [SerializeField] private TextMeshProUGUI collectableText; 
    private int totalCollectablesInLevel;
    private int collectedItems;
    void Start()
    {
        collectableText.text = "";
        collectedItems = 0;

        FindAllCollectablesInLevel();
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAllCollectablesInLevel();
    }

    private void FindAllCollectablesInLevel()
    {
        CollectableObject[] collectables = FindObjectsOfType<CollectableObject>();
        totalCollectablesInLevel = collectables.Length;

        if(totalCollectablesInLevel == 0 )
        {
            HideCollectionBar();
        }
        else
        {
            ShowCollectionBar();
        }

        collectedItems = 0; 
        UpdateCollectableUI();
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateCollectableUI();
    }

    private void UpdateCollectableUI()
    {
        collectableText.text = "" + collectedItems + "/" + totalCollectablesInLevel;
    }

    public void ShowCollectionBar()
    {
        collectionBar.SetActive(true);
    }

    public void HideCollectionBar()
    {
        collectionBar.SetActive(false);
    }
}
