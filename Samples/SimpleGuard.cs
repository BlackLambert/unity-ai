using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.AI.Samples
{
    public class SimpleGuard : Agent, Injectable, Initializable
    {
        private Observable<string> _currentActionLog;
        
        [SerializeField] 
        [Range(3, 10)] 
        private int _damage = 5;
        
        [SerializeField] 
        [Range(10, 30)] 
        private int _minEnemyHealth = 15;
        
        [SerializeField] 
        [Range(50, 100)] 
        private int _maxEnemyHealth = 75;
        
        private bool _enemyInSight;
        private bool _enemyInRange;
        private int _enemyLife;

        private Selector _selector;
        private Random _random;

        public void Inject(Resolver resolver)
        {
            _random = new Random();
            _currentActionLog = resolver.Resolve<Observable<string>>();
        }

        public void Initialize()
        {
            Init();
        }

        private void Init()
        {
            Condition inSightCondition = new Condition(() => _enemyInSight);
            Condition inRangeCondition = new Condition(() => _enemyInSight && _enemyInRange);
            Condition enemyDeadCondition = new Condition(() => _enemyInRange && _enemyInSight && _enemyLife <= 0);
            Condition enemyAliveCondition = new Condition(() => _enemyInRange && _enemyInSight && _enemyLife > 0);
            
            Action searchForTarget = new Action(() =>
            {
                _enemyLife = _random.Next(_minEnemyHealth, _maxEnemyHealth);
                _enemyInSight = true;
                _currentActionLog.Value = "Searching for an enemy... Found one!";
                return true;
            });
            
            Action runToTarget = new Action(() =>
            {
                _enemyInRange = true;
                _currentActionLog.Value = "Running to enemy... He comes in range";
                return true;
            });
            
            Action attackTarget = new Action(() =>
            {
                _enemyLife -= _damage;
                string log = $"Attacking Enemy with {_damage}.\n";
                log += _enemyLife > 0 ? $"The enemy has {_enemyLife} health left" : $"The enemy died.";
                _currentActionLog.Value = log;
                return true;
            });
            
            Action returnToPatrol = new Action(() =>
            {
                _enemyInSight = false;
                _enemyInRange = false;
                _currentActionLog.Value = "Returning to patrol";
                return true;
            });

            Sequence searchForTargetSequence = new Sequence()
                .With(searchForTarget);
            
            Sequence runToTargetSequence = new Sequence()
                .With(inSightCondition)
                .With(runToTarget);
            
            Sequence attackSequence = new Sequence()
                .With(inRangeCondition)
                .With(enemyAliveCondition)
                .With(attackTarget);
            
            Sequence returnToPatrolSequence = new Sequence()
                .With(enemyDeadCondition)
                .With(returnToPatrol);

            _selector = new Selector()
                .With(returnToPatrolSequence)
                .With(attackSequence)
                .With(runToTargetSequence)
                .With(searchForTargetSequence);
        }

        public override void Act()
        {
            if (!_selector.Execute())
            {
                Debug.LogError("The agent can't perform any action");
            }
        }
    }
}