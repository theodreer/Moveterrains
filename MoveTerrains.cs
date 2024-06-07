using UnityEngine;

public class MoveTerrains : MonoBehaviour
{
    public float scrollSpeed = 50.0f; // Velocidade do movimento
    public Terrain[] terrains; // Array para armazenar os tr�s terrenos
    private float terrainLength; // Comprimento do terreno ao longo do eixo z

    void Start()
    {
        if (terrains.Length != 3)
        {
            Debug.LogError("Por favor, atribua exatamente 3 terrenos.");
            return;
        }

        // Calcula o comprimento do terreno ao longo do eixo z
        terrainLength = terrains[0].terrainData.size.z;
    }

    void Update()
    {
        // Move cada terreno para tr�s no eixo negativo z
        foreach (Terrain terrain in terrains)
        {
            terrain.transform.position += Vector3.back * scrollSpeed * Time.deltaTime;

            // Reposiciona o terreno se ele sair da �rea vis�vel
            if (terrain.transform.position.z < -terrainLength)
            {
                RepositionTerrain(terrain);
            }
        }
    }

    void RepositionTerrain(Terrain terrain)
    {
        // Encontra o terreno mais � frente
        Terrain frontTerrain = terrains[0];
        foreach (Terrain t in terrains)
        {
            if (t.transform.position.z > frontTerrain.transform.position.z)
            {
                frontTerrain = t;
            }
        }

        // Reposiciona o terreno para logo � frente do terreno mais � frente
        terrain.transform.position = new Vector3(
            terrain.transform.position.x,
            terrain.transform.position.y,
            frontTerrain.transform.position.z + terrainLength
        );
    }
}
