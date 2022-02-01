using JoostenProductions;
using Profile;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;

    private Dictionary<int, GameObject> _touchTrails = new Dictionary<int, GameObject>();
    private Queue<GameObject> _trails = new Queue<GameObject>();

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    protected override void OnDispose()
    {
        foreach (var trail in _trails)
        {
            GameObject.Destroy(trail);
        }
        _trails.Clear();
        _touchTrails.Clear();
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        base.OnDispose();
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        AddTouchTrail(touch);
                        break;
                    case TouchPhase.Ended:
                        DeleteTouchTrail(touch);
                        break;
                }
            }
        }
    }

    private void AddTouchTrail(Touch touch)
    {
        if (!_touchTrails.ContainsKey(touch.fingerId))
        {
            if (_trails.Count == 0)
                _trails.Enqueue(CreateNewTrailRenderer());
            _touchTrails.Add(touch.fingerId, _trails.Dequeue());
        }
        _touchTrails[touch.fingerId].gameObject.transform.position = new Vector3((Camera.main.ScreenToWorldPoint(touch.position)).x, (Camera.main.ScreenToWorldPoint(touch.position)).y, 1);
        _touchTrails[touch.fingerId].SetActive(true);
    }

    private void DeleteTouchTrail(Touch touch)
    {
        if (!_touchTrails.ContainsKey(touch.fingerId))
            return;
        else
        {
            _touchTrails[touch.fingerId].SetActive(false);
            _trails.Enqueue(_touchTrails[touch.fingerId]);
            _touchTrails.Remove(touch.fingerId);
        }
    }

    private GameObject CreateNewTrailRenderer()
    {
        var go = new GameObject();
        //go.layer = SortingLayer.NameToID("UI");
        var tr = go.AddComponent<TrailRenderer>();
        //tr.sortingLayerName = "UI";
        //tr.sortingOrder = 1;
        tr.startColor = Color.white;
        tr.endColor = Color.black;
        tr.material = new Material(Shader.Find("Sprites/Default"));
        tr.time = 0.2f;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.0f);
        curve.AddKey(0.3f, 0.5f);
        curve.AddKey(0.7f, 0.2f);
        curve.AddKey(1.0f, 0.0f);

        tr.widthCurve = curve;

        return go;
    }
}

