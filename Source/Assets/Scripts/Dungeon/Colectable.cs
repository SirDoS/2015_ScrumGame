using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dungeon
{
    /// <summary>
    /// Classe que representa itens coletáveis.
    /// </summary>
    public class Colectable : MonoBehaviour, IPoolObject
    {
        /// <summary>
        /// Evento indicando Despawn de um objeto
        /// </summary>
        public EventHandler<EventArgs> onDespawn;
        /// <summary>
        /// Nome do coletável
        /// </summary>
        public string Name;

        /// <summary>
        /// Valor deste coletivel.
        /// </summary>
        public int Amount;

        private SpawnPool myPool;

        public Transform getPoolTransform()
        {
            return myPool.gameObject.transform;
        }

        /// <summary>
        /// Remove o objeto ao ser coletado.
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerEnter2D(Collider2D other)
        {
            this.gameObject.transform.parent = getPoolTransform();
            myPool.Despawn(this.gameObject);
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region PoolImplemetation
        void IPoolObject.OnSpawn(SpawnPool pMyPool)
        {
            myPool = pMyPool;
        }

        void IPoolObject.Despawn()
        {
            myPool.Despawn(this.gameObject);
        }

        void IPoolObject.DespawnIn(float fDelay)
        {
            //throw new NotImplementedException();
        }

        void IPoolObject.OnDespawn()
        {
            //throw new NotImplementedException();
            Debug.Log("Being Despawned!");
            if (onDespawn != null)
            {
                EventArgs args = new EventArgs();
                onDespawn(this, args);
            }
        }
        #endregion
    }
}
