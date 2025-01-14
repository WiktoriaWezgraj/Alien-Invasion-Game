using Simulator.Maps;

namespace Simulator;

public class SimulationHistory
{
    public Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {

        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = string.Empty,
            Move = string.Empty,
            Symbols = _simulation.Mappables
            .GroupBy(m => m.Position)
            .ToDictionary(
                group => group.Key,
                group => group.Count() > 1 ? 'X' : group.First().Symbol
            )
        });

        while (!_simulation.Finished)
        {
            var currentMappable = _simulation.CurrentMappable;
            var move = _simulation.CurrentMoveName;

            _simulation.Turn();

            TurnLogs.Add(new SimulationTurnLog
            {
                Mappable = currentMappable.ToString(),
                Move = move,
                Symbols = _simulation.Mappables
                            .GroupBy(m => m.Position)
                            .ToDictionary(
                                group => group.Key,
                                group => group.Count() > 1 ? 'X' : group.First().Symbol
                            )
            });
        }
    }
}


