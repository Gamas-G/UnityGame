using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); //Lista que contiene todos los niveles que se han creado
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();//Lista de los bloques que tenemos ahora mismo en pantalla
    public Transform levelInitialPoint; //Punto inicial donde empezará a crearse el primer nivel de todos

    private bool isGeneratingInitialBlock = false;
    private void Awake()
    {
        sharedInstance = this; //Solo necesito un generador de niveles, para ello usamos singleton
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateInitialBlocks()
    {
        isGeneratingInitialBlock = true;

        for (int i = 0; i < 3; i++)
            AddNewBlock();

        isGeneratingInitialBlock = false;
    }

    public void AddNewBlock()
    {
        //seleccionar un bloque aleatorio entre los que tenemos disponibles
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        if (isGeneratingInitialBlock)
            randomIndex = 0;

        /*
         * Creamos una copia de la instancia y no una nueva, la clase 'Instantiate' nos ayuda a clonar una instancia un objeto
         * en este caso un objeto del List de tipo 'LevelBlock' y por tanto lo necesitamos hacer un casting(Alparecer el casteo es inceseraio para el intelligense)
         */
        LevelBlock block = /*(LevelBlock)*/Instantiate(allTheLevelBlocks[randomIndex]);
        /*
         * Nos aseguramos que quede dentro del paquete de SimpleLevelBlock
         * Ejemplo cuando creamos un nuevo objeto este se crea fuera, no tiene un  padre  y no sabemos cual es su la propoedad transform (su posision en el espacio)
         * por ello lo agregamos en la lista del paquete de SimpleLevelBlock
         */
        block.transform.SetParent(this.transform, false);

        //Posicion del bloque. Recuerda que un objeto tendra una posicion 3D por lo cual es el Vector3, en cambio una fuerza es Vector2 ya que la fisica solo usa x,y
        Vector3 blockposition = Vector3.zero;


        if (currentLevelBlocks.Count == 0)
            //Vamos a colocar el primer bloque en pantalla
            blockposition = levelInitialPoint.position;
        else
            //Ya tengo bloques en pantalla, y lo colocamos al ultimo en pantalla
            blockposition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;


        block.transform.position = blockposition;
        currentLevelBlocks.Add(block);
    }
    public void RemoveOldBlock()
    {
        LevelBlock block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        Destroy(block.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while(currentLevelBlocks.Count > 0)
            RemoveOldBlock();
    }
}
