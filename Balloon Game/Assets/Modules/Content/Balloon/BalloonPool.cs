using System.Collections.Generic;
using UnityEngine;

public class BalloonPool : MonoBehaviour
{
    [SerializeField] private GameObject _balloonPrefab;
    [SerializeField] private Transform _poolParent;

    private Queue<Balloon> _balloonQueue = new();
    private List<Balloon> _activeBalloons = new(); 

    public void PopulatePool(int count)
    {
        _balloonQueue.Clear();
        _activeBalloons.Clear();

        for (int i = 0; i < count; i++)
        {
            var balloon = Instantiate(_balloonPrefab, _poolParent).GetComponent<Balloon>();
            balloon.gameObject.SetActive(false);
            _balloonQueue.Enqueue(balloon);
        }
    }

    public Balloon GetBalloon()
    {
        if (_balloonQueue.Count == 0) return null;

        var balloon = _balloonQueue.Dequeue();
        balloon.gameObject.SetActive(true);
        _activeBalloons.Add(balloon);
        return balloon;
    }

    public void ClearAll()
    {
        foreach (var balloon in _activeBalloons)
        {
            balloon.gameObject.SetActive(false);
            _balloonQueue.Enqueue(balloon);
        }
        _activeBalloons.Clear();
    }
}