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
                newShapeObj.GetComponent<SphereShape3D>().entitySpawner = this;
            }
            else
            {
                newShapeObj = SpawnCube();
            }

            RandomizeShapeColor(newShapeObj);
            RandomizeShapeSpeed(newShapeObj);
            newShapeObj.transform.SetParent(entityParentObj.transform);

        }

        entitiesSpawned += batchEntities;

        UpdateSpawnCountLabel();
    }

    private void RandomizeShapeSpeed(GameObject newShapeObj)
    {
        BaseShape3D baseShape3D = newShapeObj.GetComponent<BaseShape3D>();
        baseShape3D.Speed = Random.Range(5, 15);
    }

    private void UpdateSpawnCountLabel()
    {
        spawnCountLabel.text = "Entities Spawned: " + entitiesSpawned;
    }

    // ABSTRACTION: Abstract shape color randomization
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

    private Vector3 GetRandomSpawnPos()
    {
        Vector3 spawnPos = Random.insideUnitCircle * 40;
        spawnPos.z = spawnPos.y;
        spawnPos.y = transform.position.y + Random.Range(-10f, 10f);

        return spawnPos;
    }

    private GameObject SpawnSphere()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        return Instantiate(Sphere, spawnPos, Quaternion.identity);
    }

    private GameObject SpawnCube()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        return Instantiate(Cube, spawnPos, Quaternion.identity);
    }

    public void EntityDestroyed()
    {
        entitiesSpawned -= 1;
        UpdateSpawnCountLabel();
    }

}
