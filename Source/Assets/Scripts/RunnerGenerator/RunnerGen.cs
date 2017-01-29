using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Dungeon;
using System;

/// <summary>
/// Classe responsavel por criar blocos que são utilizados para criar a fase Runner
/// 
/// </summary>
public class RunnerGen : MonoBehaviour {

    #region config

    /// <summary>
    /// Tile utilizada para gerar o bloco.
    /// </summary>
    public GameObject tile;

    /// <summary>
    /// Largura da tile em pixels, ex: 0.32 = 32 pixels
    /// </summary>
    public float tileWidth;
    /// <summary>
    /// Altura da tile em pixels, ex: 0.32 = 32 pixels
    /// </summary>
    public float tileHeight;

    /// <summary>
    /// Quantia máxima em altura de tiles que um bloco pode ter.
    /// </summary>
    public int maxHeight = 5;

    /// <summary>
    /// Altura do pulo do jogador em quantia de tiles que ele consegue pular.
    /// </summary>
    public int jumpHeight = 3;

    /// <summary>
    /// % de chance de ao gerar um bloco, que o proximo seja para cima.
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float upChance = 25f;

    /// <summary>
    /// % de chance de ao gerar um bloco, que o proximo esteja na mesma altura.
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float stayChance = 50f;

    /// <summary>
    /// % de chance de ao gerar um bloco, que o proximo seja para baixo.
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float downChance = 25f;
    /// <summary>
    /// Chance de spawnar um recurso
    /// </summary>
    [Range(0.0f, 100.0f)]
    public float resourceChance = 25f;

    /// <summary>
    /// Quantia de formas diferentes recursos.
    /// </summary>
    public int resourceGroups = 3;

    /// <summary>
    /// Recursos que serão distribuidos pelo mapa.
    /// </summary>
    public List<GameObject> colectables = new List<GameObject>();

    /// <summary>
    /// Lista contendo as pools dos colectaveis.
    /// </summary>
    public List<SpawnPool> pools = new List<SpawnPool>();

    /// <summary>
    /// Largura de um bloco em quantidade de tiles.
    /// </summary>
    public int blockWidth;
    /// <summary>
    /// Altura de um bloco em quantidade de tiles, deve ser maior que
    /// a altura máxima de blocos definida em maxHeight, caso não seja
    /// é definido como maxHeight automáticamente.
    /// </summary>
    public int BlockHeight;

    /// <summary>
    /// Quantia maxima de recursos por bloco.
    /// </summary>
    public int maxResources = 15;

    public int resourceGroupMax = 5;
    #endregion

    /// <summary>
    /// bloco usado para gerar a matriz que gera o bloco.
    /// </summary>
    private byte[,] blockM;

    

    private static int genAmount = 0;
    /// <summary>
    /// Gera lista com diversos blocos diferentes para serem utilizados na construção da fase.
    /// 
    /// </summary>
    /// <param name="amount">Quantia de blocos a serem gerados.</param>
    /// <returns>Lista contendo os blocos já inativos para serem clonados.</returns>
    public List<GameObject> genBlocks(int amount)
    {
        List<GameObject> ans = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            ans.Add(genBlock());
        }
        return ans;
    }

    /// <summary>
    /// Gera um bloco para ser utilizado na construção da fase com base nas variaveis definidas..
    /// </summary>
    /// <returns> Bloco para ser utilizado na geração da fase.</returns>
    public GameObject genBlock()
    {
        fixChances();
        blockM = new byte[blockWidth, BlockHeight];
        //Debug.Log("Width: " + blockWidth + " Height: " + BlockHeight);
        int yGen = 0;
        float choice = 0;
        for (int i = 0; i < blockM.GetLength(0); i++)
        {
            int deviation = UnityEngine.Random.Range(1, jumpHeight);
            choice = UnityEngine.Random.Range(0, 100);
            if (choice < upChance)
            {
                yGen += deviation;
            }
            else
            {
                if (choice < upChance + downChance)
                    yGen -= deviation;
            }
            //Ajuda os limites.
            yGen = yGen < 0 ? 0 : yGen;
            yGen = yGen > maxHeight ? maxHeight : yGen;

            //posiciona os blocos.
            for (int j = 0; j <= yGen; j++)
            {
                blockM[i, j] = 1;
            }
        }
        //Debug.Log("Generated BlockM l0: " + blockM.GetLength(0) + " l1: " + blockM.GetLength(1));
        //Cria o objeto que ira conter os outros..
        GameObject obj = new GameObject("Block" + genAmount++ );
        {
            //Adiciona boxCollider que ira tratar as colisões ao sair/entrar de um bloco na esquerda.
            BoxCollider2D bc2d = obj.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc2d.size = new Vector2(blockWidth, tileHeight * BlockHeight * maxHeight );
            bc2d.offset = new Vector2( (blockWidth - 1) / 2f , 1f);
            bc2d.isTrigger = true;
        }
         //Alterações necessárias na tile.
        DungeonTile dt = obj.AddComponent<DungeonTile>() as DungeonTile;
        dt.Width = blockWidth * tileWidth;
        



        for (int i = 0; i < blockM.GetLength(0); i++)
        {
            for (int j = 0; j < blockM.GetLength(1); j++)
            {
                if (blockM[i,j] == 1)
                {
                    //Gera recursos.

                    if (j + 2 < blockM.GetLength(1) && blockM[i, j + 1] != 1)
                    {
                        if (UnityEngine.Random.Range(0f, 100f) < resourceChance) 
                        {
                            //SpawnPool chosenPool = pools[Random.Range(0, pools.Count - 1)];
                            //GameObject chosenSource = colectables[Random.Range(0, colectables.Count - 1)];
                            int randAmount = UnityEngine.Random.Range(1, resourceGroupMax);
                            for (int k = 0; k < randAmount; k++)
                            {
                                //Spawna e adiciona a um dos grupos de recursos.
                                Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range((i * tileWidth), ((i + 1) * tileWidth)),
                                    UnityEngine.Random.Range((j + 1) * tileHeight, (j + 2) * tileHeight));
                                dt.ResourcePoints.Add(spawnPoint);
                            }
                        }
                    }
                    

                    //adiciona objeto.
                    Vector2 spPoint = new Vector2(i * tileWidth, j * tileHeight);
                    GameObject child = (GameObject)GameObject.Instantiate(tile, spPoint, Quaternion.identity);
                    child.transform.parent = obj.transform;
                    //Debug.Log("Creating child, x: " + (i * tileWidth) + " Y: " + (j * tileHeight));
                }
            }
        }
        //Debug.Log("Finished my work.");
        obj.SetActive(false);
        return obj;
    }

    /*private void SpawnResources(object sender, EventArgs e)
    {
        Debug.Log("Spawning gold!");
        DungeonTile dt = sender as DungeonTile;
        int i = UnityEngine.Random.Range(0, dt.ResourcePoints.Count);
        int j = UnityEngine.Random.Range(i, dt.ResourcePoints.Count);
        for (; i < j; i++)
        {
            pools[UnityEngine.Random.Range(0, pools.Count)].Spawn(dt.ResourcePoints[i], Quaternion.identity);
        }
    }*/

    /// <summary>
    /// Ajusta as chances para não ultrapassarem o range de 0-100.
    /// Tal ajuste força os valores a manterem sua mesma proporção
    /// porem dentro do range de 0-100.
    /// </summary>
    private void fixChances()
    {
        float sumChances = upChance + stayChance + downChance;
        //Ajusta chances caso ultrapasse 100.
        if (sumChances > 100f)
        {
            //Calcula a proporção de cada chance
            float uCprop = upChance / sumChances;
            float sCprop = stayChance / sumChances;
            float dCprop = downChance / sumChances;
            //Ajusta para a proporção correta.
            upChance *= uCprop;
            stayChance *= sCprop;
            downChance *= dCprop;
        }
    }

    // Use this for initialization
    void Start () {
        //genBlock();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
