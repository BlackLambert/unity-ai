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

        private Node _selector;
        private Random _random;
        private MutableLog _log;

        public void Inject(Resolver resolver)
        {
            _random = new Random();
            _currentActionLog = resolver.Resolve<Observable<string>>();
            _log = resolver.Resolve<MutableLog>();
        }

        public void Initialize()
        {
            Init();
        }

        private void Init()
        {
            Node inSightCondition = new Condition(() => _enemyInSight)
                .WithId(0)
                .WithName("Is enemy in sight?")
                .Logged(_log);
            
            Node inRangeCondition = new Condition(() => _enemyInSight && _enemyInRange)
                .WithId(1)
                .WithName("Is enemy in range?")
                .Logged(_log);
            
            Node enemyDeadCondition = new Condition(() => _enemyInRange && _enemyInSight && _enemyLife <= 0)
                .WithId(2)
                .WithName("Is enemy dead?")
                .Logged(_log);
            
            Node enemyAliveCondition = new Condition(() => _enemyInRange && _enemyInSight && _enemyLife > 0)
                .WithId(3)
                .WithName("Is enemy still alive?")
                .Logged(_log);
            
            Node searchForTarget = new Action(() =>
            {
                _enemyLife = _random.Next(_minEnemyHealth, _maxEnemyHealth);
                _enemyInSight = true;
                _currentActionLog.Value = "Searching for an enemy... Found one!";
                return true;
            }).WithId(10).WithName("Search for target").Logged(_log);
            
            Node runToTarget = new Action(() =>
            {
                _enemyInRange = true;
                _currentActionLog.Value = "Running to enemy... He comes in range";
                return true;
            }).WithId(11).WithName("Run towards target").Logged(_log);
            
            Node attackTarget = new Action(() =>
            {
                _enemyLife -= _damage;
                string log = $"Attacking Enemy with {_damage}.\n";
                log += _enemyLife > 0 ? $"The enemy has {_enemyLife} health left" : $"The enemy died.";
                _currentActionLog.Value = log;
                return true;
            }).WithId(12).WithName("Attack enemy").Logged(_log);
            
            Node returnToPatrol = new Action(() =>
            {
                _enemyInSight = false;
                _enemyInRange = false;
                _currentActionLog.Value = "Returning to patrol";
                return true;
            }).WithId(13).WithName("Resume patrol").Logged(_log);

            Node searchForTargetSequence = new Sequence()
                .With(searchForTarget)
                .WithId(ActionType.SearchForTarget)
                .WithName("Try searching for target")
                .Logged(_log);
            
            Node runToTargetSequence = new Sequence()
                .With(inSightCondition)
                .With(runToTarget)
                .WithId(ActionType.RunToTarget)
                .WithName("Try running towards target")
                .Logged(_log);
            
            Node attackSequence = new Sequence()
                .With(inRangeCondition)
                .With(enemyAliveCondition)
                .With(attackTarget)
                .WithId(ActionType.Attack)
                .WithName("Try attacking target")
                .Logged(_log);
            
            Node returnToPatrolSequence = new Sequence()
                .With(enemyDeadCondition)
                .With(returnToPatrol)
                .WithId(ActionType.ReturnToPatrol)
                .WithName("Try resuming patrol")
                .Logged(_log);

            _selector = new Selector()
                .With(returnToPatrolSequence)
                .With(attackSequence)
                .With(runToTargetSequence)
                .With(searchForTargetSequence)
                .WithId(1000)
                .WithName("Guard actions")
                .Logged(_log)
                .ConsoleLogged(_log);
        }

        public override void Act()
        {
            if (!_selector.Execute())
            {
                Debug.LogError("The agent can't perform any action");
            }
        }
        
        public enum ActionType
        {
            SearchForTarget = 100,
            RunToTarget = 101,
            Attack = 102,
            ReturnToPatrol = 103
        }
    }
}