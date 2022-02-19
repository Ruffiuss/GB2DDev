using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class ItemConfig : ScriptableObject, IConfig
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private string _title;

        public int Id => _id;

        public string Title => _title;
    }
}