

namespace Assets.Scripts.Dungeon
{
    public class Resource
    {
        /// <summary>
        /// Quantia existente do Recurso.
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Nome do recurso
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Representação em texto deste recurso.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + ": " + Amount;
        }
    }
}
