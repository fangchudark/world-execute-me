using Godot;
using System;

public partial class Main : Control
{
    [Export] private PackedScene _entry;
    [Export] private SimulationStyle _simulationStyle;
    [Export] private Button _startButton;
    [Export] private OptionButton _styleOptionButton;

    public override void _Ready()
    {
        _startButton.Pressed += OnStartButtonPressed;
        _styleOptionButton.ItemSelected += OnStyleOptionButtonItemSelected;        
    }

    private void OnStartButtonPressed()
    {        
        var entry = _entry.Instantiate<Entry>();
        entry.SimulationStyle = _simulationStyle;
        GetTree().Root.AddChild(entry);
        this.QueueFree();
    }

    private void OnStyleOptionButtonItemSelected(long index)
    {
        _simulationStyle = (SimulationStyle)index;
    }
}
