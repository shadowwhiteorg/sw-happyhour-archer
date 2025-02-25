using System;
using System.Collections.Generic;
using _Game.GameMechanics;
using _Game.Interfaces;
using _Game.Utils;
using UnityEngine;

namespace _Game.Core
{
    public class CombatManager : Singleton<CombatManager>
    {
        private QuadTree<EnemyCharacter> _enemyQuadTree;
        private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();

        private void Start()
        {
            _enemyQuadTree = new QuadTree<EnemyCharacter>(new Rect(-50,-50,100,100), 4);
        }

        private void OnEnable()
        {
            EventManager.OnSearchEnemies += UpdateAllEnemyPositions;
        }

        private void OnDisable()
        {
            EventManager.OnSearchEnemies -= UpdateAllEnemyPositions;
        }
        
        public void AddEnemy(EnemyCharacter enemy)
        {
            _enemies.Add(enemy);
            _enemyQuadTree.Insert(enemy);
        }
        
        public void RemoveEnemy(EnemyCharacter enemy)
        {
            _enemies.Remove(enemy);
            _enemyQuadTree.Remove(enemy);
        }

        private void UpdateAllEnemyPositions()
        {
            _enemyQuadTree = new QuadTree<EnemyCharacter>(new Rect(-50, -50, 100, 100), 4);
            foreach (var enemy in _enemies)
            {
                _enemyQuadTree.Insert(enemy);
            }
        }

        public IDamageable FindNearestEnemy(Vector3 position, float searchRadius)
        {
            EventManager.FireOnEnemySearch();
            return _enemyQuadTree.FindNearest(position, searchRadius);
        }
        
        
    }
}