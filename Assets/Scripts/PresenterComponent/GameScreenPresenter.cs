using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class GameScreenPresenter : IInitializable
{
    private readonly GameScreenView _gameScreenView;

    public GameScreenPresenter(GameScreenView gameScreenView)
    {
        _gameScreenView = gameScreenView;
    }

    public void Initialize() { }
}