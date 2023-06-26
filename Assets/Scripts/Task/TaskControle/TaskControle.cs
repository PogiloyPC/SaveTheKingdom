using CountryModifi;
using StructHouse;
using InterfaceTask;
using UnitStruct;

public class TaskControle : IDistributeTasks
{       
    private PoolTasks<UnitCitizenTask, ITask> _stoneTasksPool = new PoolTasks<UnitCitizenTask, ITask>();
    private PoolTasks<UnitCitizenTask, ITask> _woodTasksPool = new PoolTasks<UnitCitizenTask, ITask>();
    private PoolTasks<UnitCitizenTask, ITask> _buildTasksPool = new PoolTasks<UnitCitizenTask, ITask>();
    private PoolTasks<UnitCitizenTask, ITask> _farmTasksPool = new PoolTasks<UnitCitizenTask, ITask>();
    private PoolTasks<UnitCitizenTask, ITask> _lakeTasksPool = new PoolTasks<UnitCitizenTask, ITask>();

    public void CheckUnitProfession(UnitCitizen unit)
    {
        switch (unit.TypeUnit)
        {
            case TypeUnitCitizen.Farmer:
                UnitCitizenTask farmer = (UnitCitizenTask)unit;

                farmer.GetTaskControle(_farmTasksPool);

                _farmTasksPool.GetUnit(farmer);
                break;
            case TypeUnitCitizen.Fisher:
                UnitCitizenTask fisher = (UnitCitizenTask)unit;

                fisher.GetTaskControle(_lakeTasksPool);

                _lakeTasksPool.GetUnit(fisher);
                break;
            case TypeUnitCitizen.Bricklayer:
                UnitCitizenTask bricklayer = (UnitCitizenTask)unit;

                bricklayer.GetTaskControle(_stoneTasksPool);

                _stoneTasksPool.GetUnit(bricklayer);
                break;
            case TypeUnitCitizen.Lumberman:
                UnitCitizenTask lumberman = (UnitCitizenTask)unit;

                lumberman.GetTaskControle(_woodTasksPool);

                _woodTasksPool.GetUnit(lumberman);
                break;
            case TypeUnitCitizen.Carpenter:
                UnitCitizenTask carpenter = (UnitCitizenTask)unit;

                carpenter.GetTaskControle(_buildTasksPool);

                _buildTasksPool.GetUnit(carpenter);
                break;
            default:
                break;
        }
    }

    public void DistributeTasks(ITask task)
    {
        switch (task)
        {
            case StoneMining:
                _stoneTasksPool.GetTasks((StoneMining)task);
                break;
            case Cutting:
                _woodTasksPool.GetTasks((Cutting)task);
                break;
            case Weed:
                _farmTasksPool.GetTasks((Weed)task);
                break;
            case Fishing:
                _lakeTasksPool.GetTasks((Fishing)task);
                break;
            case ControleHouse:
                _buildTasksPool.GetTasks((ControleHouse)task);
                break;
            default:
                break;
        }
    }   
}
