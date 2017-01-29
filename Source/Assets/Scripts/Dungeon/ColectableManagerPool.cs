using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Dungeon;

public class ColectableManagerPool : MonoBehaviour {
    /// <summary>
    /// Representa um coletavel.
    /// </summary>
    


    /// <summary>
    /// Lista de objetos coletaveis que serão mantidos;
    /// </summary>
    public List<SpawnPool> colectablePool = new List<SpawnPool>();

    public Text GoldText;

    private List<Resource> resourceList;

    public SpawnPool enemyPools;

    // Use this for initialization
    void Start () {
        resourceList = new List<Resource>();
        foreach (SpawnPool pool in colectablePool)
        {
            Colectable col = pool.m_objectPrefab.GetComponent<Colectable>();
            col.onDespawn += addResource;
            Resource r = new Resource() { Name = col.name, Amount = 0};
            resourceList.Add(r);
        }
    }
   
    

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            float xRan = UnityEngine.Random.Range(1, 10);
            float yRan = UnityEngine.Random.Range(0, 1.5f);
            
            GameObject obj =  enemyPools.Spawn(new Vector3(xRan, yRan, 0f), Quaternion.identity);
            Colectable col =  obj.GetComponent<Colectable>();
            col.onDespawn += addResource;
        }

    }


    /// <summary>
    /// Adiciona um recurso com base no coletavel coletado!
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void addResource(object sender, EventArgs args)
    {
        Colectable s = sender as Colectable;
        foreach (Resource r in resourceList)
        {
            if (r.Name.Equals(s.Name))
            {
                r.Amount += s.Amount;
                if (r.Name.Equals("Gold"))
                {
                    GoldText.text = r.ToString();
                }
                break;
            }
        }
        //Debug.Log("String: " + ToString());
    }


    public override string ToString()
    {
        string answer = "";
        foreach (Resource r in resourceList)
        {
            answer += r.ToString() + "\n";
        }
        return answer;
    }
}
