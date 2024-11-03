using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitySpawner : MonoBehaviour
{
    public GameObject Sphere;
    public GameObject Cube;


    public GameObject entityParentObj;

    private int entitiesSpawned = 0;
    [SerializeField] private int batchEntities = 500;


    public TextMeshProUGUI spawnCountLabel;

    public void SpawnShape()
    {
        for (int i = 0; i < batchEntities; i++)
        {
            GameObject newShapeObj;

            if (Random.value > 0.5f)
            {
                newShapeObj = SpawnSphere();
            }
            else
            {
                newShapeObj = SpawnCube();
            }

            RandomizeShapeColor(newShapeObj);

            newShapeObj.transform.SetParent(entityParentObj.transform);
            
        }

        entitiesSpawned += batchEntities;


        spawnCountLabel.text = "Entities Spawned: " + entitiesSpawned;
    }

    private void RandomizeShapeColor(GameObject newShapeObj)
    {
        // Set color
        MeshRenderer meshRenderer = newShapeObj.GetComponent<MeshRenderer>();
        Material oldMeshMaterial = meshRenderer.material;
        Material newMeshMaterial = new Material(oldMeshMaterial);

        newMeshMaterial.color = GenerateRandomColor();
        meshRenderer.material = newMeshMaterial;
    }

    private Color GenerateRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    private GameObject SpawnSphere()
    {
        Vector3 spawnPos = Random.insideUnitCircle;
        spawnPos.y = transform.position.y;
        return Instantiate(Sphere, spawnPos, Quaternion.identity);
    }


    private GameObject SpawnCube()
    {
        Vector3 spawnPos = Random.insideUnitCircle;
        spawnPos.y = transform.position.y;
        return Instantiate(Cube, spawnPos, Quaternion.identity);
    }

}
