using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] // update when play or edit mode
[RequireComponent(typeof(TextMeshPro))]
public class Coordinate : MonoBehaviour

{
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(230, 123, 16);
    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color blockedColor = Color.gray;
    private int editorGridSettings = 10;


    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinate();
    }


    void Update()
    {
        if (Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }

        ToggleLabels();
        SetLabelColor();
    }

    void SetLabelColor()
    {
        if (gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            return;
        }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }

        else if (node.isPath)
        {
            label.color = pathColor;
        }

        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinate()
    {
        if (gridManager == null)
        {
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / editorGridSettings);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / editorGridSettings);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive(); //toggling *if label is active make it unenabled otherwise the opposite*
        }
    }
}