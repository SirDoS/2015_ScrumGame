using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dungeon;
using System;

/// <summary>
/// Classe que gerencia a pool de blocos utilizados para criar a fase.
/// </summary>
public class DungeonManager : MonoBehaviour {

	private static DungeonManager instance;
	public List<DungeonTile> dungeonTiles = new List<DungeonTile>();
	public List<SpawnPool> tilesPools = new List<SpawnPool>();
    public List<SpawnPool> enemyPools = new List<SpawnPool>();



    public int maxEnemiesPerBlock = 3;
    /// <summary>
    /// Maximo de tiles possiveis de estarem ativas num mesmo tempo.
    /// </summary>
    public const int MAX_TILES = 7;
    /// <summary>
    /// Quantia de tiles diferentes existentes.
    /// </summary>
    public int tileAmount = 5;
    /// <summary>
    /// Whatever.
    /// </summary>
	public int theFinalCountDown = 5;
    /// <summary>
    /// Indica se já terminou o jogo ou não.
    /// </summary>
	public bool gameOver;

    public int distanceToCreate = 2;

    private float blockWidth = 0;
    

    private Transform lastTile;

    /// <summary>
    /// Lista contendo o index do tipo de block que foi criado durante a run.
    /// </summary>
    private List<int> blockIndexer = new List<int>();

    private int indexerPos = -1;
    private int leftAmount = 2;

    public static DungeonManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType(typeof(DungeonManager)) as DungeonManager;
			}
			return instance;
		}

    }

    void Update()
    {
        //Debug.DrawLine(Vector3.zero, new Vector3(indexerPos, indexerPos));
    }

    void Start(){
        RunnerGen g = gameObject.GetComponent<RunnerGen>() as RunnerGen;
        blockWidth = g.blockWidth * g.tileWidth;
        ColectableManagerPool cmp = GetComponent<ColectableManagerPool>();
        //for
        //g.colectables 

        GameObject pools = new GameObject("Pools");
        pools.transform.parent = this.gameObject.transform;
        PoolManager pm = pools.AddComponent<PoolManager>() as PoolManager;

        int i = 0;
        foreach (GameObject o in g.genBlocks(tileAmount))
        {
            //Cria pools dos objetos gerados
            //o.SetActive(true);
            GameObject pool = new GameObject("Pool_" + i);
            pool.transform.parent = pools.transform;
            SpawnPool sp = pool.AddComponent<SpawnPool>() as SpawnPool ;
            sp.m_adoptChildren = true;
            sp.m_createOnStart = true;
            sp.m_poolName = "Pool_" + i++;
            sp.m_startInstances = 5;
            sp.m_extraInstances = 5;
            sp.m_objectPrefab = o;

            //Adiciona as pools a tilePools.
            tilesPools.Add(sp);
        }




        CreateStartDungeonTile(Vector3.zero);
	}

    /*
    /// <summary>
    /// Ativado quando o jogador entra em uma tile.
    /// </summary>
    /// <param name="other">Tile cuja o jogador entrou.</param>
    internal void onEnterTile(Collider2D other)
    {
        throw new NotImplementedException();
    }*/

    /// <summary>
    /// Ativado quando o jogador entra em uma tile.
    /// </summary>
    /// <param name="transTile">transform da tile que o jogador entrou.</param>
    internal void onEnterTile(Transform transTile)
    {
        if (lastTile != null)
        {
            if (transTile.position.x  < lastTile.position.x)
            {
                //Tile ao entrar é a esquerda
                indexerPos--;
                Debug.Log("Left " + indexerPos + " leftAmount: " + leftAmount);
                //Se quantia de blocos a esquerda somado a posição negativa do indexer for menor que a distancia
                //entao cria o bloco.
                if ((leftAmount +  indexerPos) < distanceToCreate)
                {
                    float x = (leftAmount) * (-blockWidth);
                    Debug.Log("Time to add Another block to the Left!" + x);
                    Vector3 vec3 = new Vector3(x, 0f);
                    CreateDungeonTile(vec3);
                    leftAmount++;
                }
            }
            else
            {
                //Right
                indexerPos++;
                Debug.Log("Right " + indexerPos);

                if ((blockIndexer.Count - leftAmount) < distanceToCreate + indexerPos)
                {
                    float x = ((blockIndexer.Count - leftAmount)) * blockWidth;
                    Debug.Log("ADD RIGHT BLOCK." + x);
                    Vector3 vec3 = new Vector3(x, 0f);
                    CreateDungeonTile(vec3);
                }
            }
        }
        //Debug.Log("");
        lastTile = transTile;
    }

    public void AddDungeonTiles(DungeonTile pDungeonTiles){
        //string log = "Starting Add: ";
        if (dungeonTiles.Count > 0)
        {
            int pos = 0;
            float ptX = pDungeonTiles.transform.position.x;
            float nxt = dungeonTiles[pos].transform.position.x;
            //log += "\n ptx: " + ptX + " next: " + nxt;
            while (ptX > nxt && pos < dungeonTiles.Count)
            {
                nxt = dungeonTiles[pos++].transform.position.x;
               // log += "\nCurpos: " + pos + "ptx: " + ptX + " next: " + nxt;
            }
            //log += "\nAdding at: " + pos + " Count is: " + dungeonTiles.Count;
            dungeonTiles.Insert(pos, pDungeonTiles);
            //Debug.Log(log);
        }
        else
        {
            dungeonTiles.Add(pDungeonTiles);
        }
		
	}

    /// <summary>
    /// Remove a tile mais longe, nao vo mudar o nome do metodo por preguiça.
    /// </summary>
	public void RemoveFirstDungeonTile(){
		if(dungeonTiles.Count > 0){
            if (indexerPos < 0) // left
            {
                //Remove right block
                dungeonTiles[dungeonTiles.Count - 1].Despawn();
                dungeonTiles.RemoveAt(dungeonTiles.Count - 1);
                Debug.Log("I am removing the rightmost tile");
            }
            else
            {
                Debug.Log("I am removing the first tile!");
                //Remove first at left.
                dungeonTiles[0].Despawn();
                dungeonTiles.RemoveAt(0);
            }
		}else{
			Debug.LogError("Remove Dungeon Tile, deu ruim, viado");
		}
	}
	
	public void CreateDungeonTile(Vector3 pPosition){
		//Debug.Log ("CreatingDungeonTile");
		int i = UnityEngine.Random.Range(0, tilesPools.Count);
        blockIndexer.Add(i);

        DungeonTile tile = tilesPools[i].Spawn<DungeonTile>(pPosition, Quaternion.identity);
        tile.gameObject.SetActive(true);

        
        //Spawna recursos 
        ColectableManagerPool cmp = GetComponent<ColectableManagerPool>();
        i = UnityEngine.Random.Range(0, tile.ResourcePoints.Count);
        int j = UnityEngine.Random.Range(i, tile.ResourcePoints.Count);
        if (j - i < 2 && i > 2)
        {
            i = 0;
        }
        for (; i < j; i++)
        {
            Vector3 point = tile.ResourcePoints[i];
            GameObject go = cmp.colectablePool[UnityEngine.Random.Range(0, cmp.colectablePool.Count)].Spawn(
                Vector3.zero, Quaternion.identity);
            go.transform.parent = tile.gameObject.transform;
            Colectable col = go.GetComponent<Colectable>();
            col.onDespawn += cmp.addResource;
            go.transform.localPosition = point;
        }

        //Spawna inimigos
        for (i = 0; i < UnityEngine.Random.Range(0, maxEnemiesPerBlock); i++)
        {
            Vector3 point = tile.ResourcePoints[UnityEngine.Random.Range(0, tile.ResourcePoints.Count - 1)];
            GameObject go = enemyPools[0].Spawn(Vector3.zero, Quaternion.identity);
            go.transform.parent = tile.gameObject.transform;
            go.transform.localPosition = point;
            go.transform.parent = enemyPools[0].gameObject.transform;
        }


        AddDungeonTiles(tile);
        

		if(dungeonTiles.Count > MAX_TILES){
			RemoveFirstDungeonTile();
		}
	}

	public void CreateStartDungeonTile(Vector3 pPosition){
        Debug.Log ("CreatingStartDungeonTile");
        int start, end;
        start = -2;
        end = 2;
        for (;start < end; start++)
        {
            Vector3 pos = new Vector3(pPosition.x - (blockWidth * start), pPosition.y);
            CreateDungeonTile(pos);
        }

	}
    public void onMoveRight()
    {
        indexerPos++;
        if (blockIndexer.Count < distanceToCreate + indexerPos)
        {
            float x = (blockIndexer.Count + 1) * blockWidth;
            Debug.Log("Time to add Another block to the right: " + x );
            Vector3 vec3 = new Vector3(x, 0f);
            CreateDungeonTile(vec3);
        }
    }
    public void onMoveLeft()
    {
        //indexerPos--;
        if (indexerPos - distanceToCreate < 0)
        {
            Debug.Log("Time to add Another block to the Left!");
            Vector3 vec3 = new Vector3((blockIndexer.Count + 1) * (-blockWidth), 0f);
            CreateDungeonTile(vec3);
        }
    }


	public void MessageInAEnter(){
		if(theFinalCountDown == 0 && !gameOver){
			gameOver = true;
		}else if (!gameOver){
			CreateDungeonTile(dungeonTiles[dungeonTiles.Count - 1].exit.position);
			theFinalCountDown--;
		}
	}

}
