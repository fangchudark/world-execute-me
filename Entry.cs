using Godot;
using System;
public partial class Entry : Control
{
    public SimulationStyle SimulationStyle
    {
        get => _simulationStyle;
        set
        {
            _simulationStyle = value;
            switch (value)
            {
                case SimulationStyle.Console:
                    _simulation = new WorldSimulationConsole(new GDTypewriter(_label));
                    break;
                case SimulationStyle.CSCode:
                    _simulation = new WorldSimulationCS(new GDTypewriter(_label));
                    break;
                case SimulationStyle.JavaCode:
                    _simulation = new WorldSimulationJava(new GDTypewriter(_label));
                    break;
            }                
 
        }
    }
    private SimulationStyle _simulationStyle = SimulationStyle.Console;
    [Export]
    private RichTextLabel _label;
    [Export]
    private AudioStreamPlayer _audioStreamPlayer;
    private VScrollBar _vScrollBar;
    private ISimulation _simulation;

    public override void _Ready()
    {
        _vScrollBar = _label.GetVScrollBar();
        _vScrollBar.Draw += _vScrollBar.Hide;

        if (FileAccess.FileExists("res://audio/world.execute(me);.wav"))
        {
            _audioStreamPlayer.Stream = ResourceLoader.Load<AudioStreamWav>("res://audio/world.execute(me);.wav");
        }
        
        StartSimulation();
    }

    public override void _ExitTree()
    {        
        _vScrollBar.Draw -= _vScrollBar.Hide;
    }
     
    public override void _Input(InputEvent @event)
    {
        if (
            (_simulation != null &&
            !_simulation.IsRunning &&
            @event is InputEventKey key &&
            !key.IsEcho()) ||
            (@event is InputEventKey esc &&
            esc.Keycode == Key.Escape)
        )
        {
            GetTree().ChangeSceneToFile("res://main.tscn");
            _simulation.Stop();            
            QueueFree();
        }
    }
    private void StartSimulation()
    {
        _simulation ??= new WorldSimulationConsole(new GDTypewriter(_label));
        _simulation.Run();
        _audioStreamPlayer.Play();
    }
}
