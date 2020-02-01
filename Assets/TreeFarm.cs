using Interact;
using UnityEngine;

public class TreeFarm : MonoBehaviour
{
    [SerializeField] private Grabable _sapling;
    private bool _playerNear;

    public Grabable GiveSapling()
    {
        return Instantiate(_sapling);
    }
}
