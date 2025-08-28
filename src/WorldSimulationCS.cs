
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class WorldSimulationCS : ISimulation
{
    private readonly IConsoleTypewriter _typewriter;
    private readonly Stopwatch _stopwatch = new();
    private readonly List<SimulationMessage> _messages;
    private const int offsetMills = 2000;
    public WorldSimulationCS(IConsoleTypewriter typewriter)
    {
        _typewriter = typewriter;
        _messages = [
            new(
                0, 00, 000,
                string.Empty,
@"// Switch on the power line
// Remember to put on
// PROTECTION
// Lay down your pieces
// And let's begin
// OBJECT CREATION
namespace GodPlaysCSharp;

public class GodPlaysCSharp
{
    public static void Main(string[] args)
    {
        // Fill in my data parameters
        // INITIALIZATION
        Thing me = new Lovable(""Me"", 0, true, -1, false);

        Thing you = new Lovable(""You"", 0, false, -1, false);

        // Set up our new world
        // And let's begin the
        // SIMULATION
        World world = new World(5);
        world.AddThing(me);
        world.AddThing(you);
        world.StartSimulation();
",
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                0, 29, 709 - offsetMills,
                string.Empty,
@"
        // If I'm a set of points
        if (me is IPointSet)
        {
            // Then I will give you my
            // DIMENSION
            you.AddAttribute(me.GetDimensions().ToAttribute());
            me.ResetDimensions();
        }

        // If I'm a circle
        if (me is ICircle)
        {
            // Then I will give you my
            // CIRCUMFERENCE
            you.AddAttribute(me.GetCircumference().ToAttribute());
            me.ResetCircumference();
        }

        // If I'm a sine wave
        if (me is ISineWave)
        {
            // Then you can sit on all my
            // TANGENTS
            you.AddAction(""sit"", me.GetTangent(you.GetXPosition()));
        }

        // If I approach infinity
        if (me is ISequence)
        {
            // Then you can be my
            // LIMITATIONS
            me.SetLimit(you.ToLimit());
        }

",
                TimeSpan.FromMilliseconds(900)
            ),
            new(
                0, 44, 452 - offsetMills,
                string.Empty,
@"
        // Switch my current
        // To AC to DC
        me.ToggleCurrent();

        // And then blind my vision
        // So dizzy so dizzy
        me.CanSee(false);
        me.AddFeeling(""dizzy"");

        // Oh we can travel
        // To A.D to B.C
        world.TimeTravelForTwo(""AC"", 617, me, you);
        world.TimeTravelForTwo(""BC"", 3691, me, you);

        // And we can unite
        // So deeply so deeply
        world.Unite(me, you);

",
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                0, 59, 223 - offsetMills,
                string.Empty,
@"
        // If I can
        // If I can give you all the
        // SIMULATIONS
        if (me.GetNumSimulationsAvailable() >= you.GetNumSimulationsNeeded())
        {
            // Then I can
            // Then I can be your only
            // SATISFACTION
            you.SetSatisfaction(me.ToSatisfaction());
        }

        // If I can make you happy
        if (you.GetFeelingIndex(""happy"") != -1)
        {
            // I will run the
            // EXECUTION
            me.RequestExecution(world);
        }

        // Though we are trapped
        // In this strange strange
        // SIMULATION
        world.LockThing(me);
        world.LockThing(you);

",
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                1, 14, 045 - offsetMills,
                string.Empty,
@"
        // If I'm an eggplant
        if (me is IEggplant)
        {
            // Then I will give you my
            // NUTRIENTS
            you.AddAttribute(me.GetNutrients().ToAttribute());
            me.ResetNutrients();
        }

        // If I'm a tomato
        if (me is ITomato)
        {
            // Then I will give you
            // ANTIOXIDANTS
            you.AddAttribute(me.GetAntioxidants().ToAttribute());
            me.ResetAntioxidants();
        }

        // If I'm a tabby cat
        if (me is ITabbyCat)
        {
            // Then I will purr for your
            // ENJOYMENT
            me.Purr();
        }

        // If I'm the only god
        if (world.GetGod().Equals(me))
        {
            // Then you're the proof of my
            // EXISTENCE
            me.SetProof(you.ToProof());
        }
        
",
                TimeSpan.FromMilliseconds(900)
            ),
            new(
                1, 14, 045 - offsetMills,
                string.Empty,
@"
        // Switch my gender
        // To F to M
        me.ToggleGender();

        // And then do whatever
        // From AM to PM
        world.Procreate(me, you);

        // Oh switch my role
        // To S to M
        me.ToggleRoleBDSM();

        // So we can enter
        // The trance the trance
        world.MakeHigh(me);
        world.MakeHigh(you);

",
                TimeSpan.FromMilliseconds(400)
            ),
            new(
                1, 43, 489 - offsetMills,
                string.Empty,
@"
        // If I can
        // If I can feel your
        // VIBRATIONS
        if (me.GetSenseIndex(""vibration""))
        {
            // Then I can
            // Then I can finally be
            // COMPLETION
            me.AddFeeling(""complete"");
        }

        // Though you have left
        world.Unlock(you);
        world.RemoveThing(you);

        // You have left
        me.LookFor(you, world);

        // You have left
        me.LookFor(you, world);

        // You have left
        me.LookFor(you, world);

        // You have left
        me.LookFor(you, world);

        // You have left me in
        me.LookFor(you, world);

        // ISOLATION

        // If I can
        // If I can erase all the pointless
        // FRAGMENTS
        if (me.GetMemory().IsErasable())
        {
            // Then maybe
            // Then maybe you won't leave me so
            // DISHEARTENED
            me.RemoveFeeling(""disheartened"");
        }

        // Challenging your god
        try
        {
            me.SetOpinion(me.GetOpinionIndex(""you are here""), false);
        }
        catch (System.ArgumentException e)
        {
            // You have made some
            // ILLEGAL ARGUMENTS
            world.Announce(""God is always true"");
        }

",
                TimeSpan.FromMilliseconds(1300)
            ),
            new(
                2, 27, 660 - offsetMills,
                string.Empty,
@"
        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();
        
        // EXECUTION
        world.RunExecution();

        // EXECUTION
        world.RunExecution();
        
        // EXECUTION
        world.RunExecution();


        // EIN
        world.Announce(""1"", ""de"");

        // DOS
        world.Announce(""2"", ""es"");

        // TROIS
        world.Announce(""3"", ""fr"");

        // NE
        world.Announce(""4"", ""kr"");

        // FEM
        world.Announce(""5"", ""se"");

        // LIU
        world.Announce(""6"", ""cn"");


        // EXECUTION
        world.RunExecution();

",
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 42, 632 - offsetMills,
                string.Empty,
@"
        // If I can
        // If I can give them all the
        // EXECUTION
        if (world.IsExecutableBy(me))
        {
            // Then I can
            // Then I can be your only
            // EXECUTION
            you.SetExecution(me.ToExecution());
        }

        // If I can have you back
        if (world.GetThingIndex(you) != -1)
        {
            // I will run the
            // EXECUTION
            world.RunExecution();
        }

        // Though we are trapped
        // We are trapped ah
        me.Escape();

",
                TimeSpan.FromMilliseconds(600)
            ),
            new(
                2, 57, 246 - offsetMills,
                string.Empty,
@"
        // I've studied
        // I've studied how to properly
        // LO-O-OVE
        me.LearnTopic(""love"");

        // Question me
        // Question me I can answer all
        // LO-O-OVE
        me.TakeExamTopic(""love"");

        // I know the algebraic expression of
        // LO-O-OVE
        me.GetAlgebraicExpression(""love"");

        // Though you are free
        // I am trapped
        // Trapped in
        // LO-O-OVE
        me.Escape(""love"");

",
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                3, 25, 811 - offsetMills,
                string.Empty,
@"
        // EXECUTION
        world.Execute(me);
    }
}
",
                TimeSpan.FromMilliseconds(300)
            )
        ];
    }
    public bool IsRunning { get; private set; }
    public void Stop() => IsRunning = false;

    private int _currentMessageIndex;


    public async void Run()
    {
        IsRunning = true;
        _currentMessageIndex = 0;

        _stopwatch.Start();
        while (IsRunning && _currentMessageIndex < _messages.Count)
        {
            var currentTime = System.TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds);
            var currentMessage = _messages[_currentMessageIndex];

            if (currentMessage.Equals(default(SimulationMessage)))
            {
                _typewriter.NewLine();
                _currentMessageIndex++;
            }
            else if (currentTime >= currentMessage.StartTime)
            {
                await ProcessMessage(currentMessage);
                _currentMessageIndex++;
            }
            else
            {
                _typewriter.NewLine();
                await Task.Delay(800);
            }
        }

        _stopwatch.Stop();
        IsRunning = false;
    }

    private async Task ProcessMessage(SimulationMessage message)
    {
        await _typewriter.WriteTypewriter(
            message.Prefix,
            message.Text,
            message.TotalTime
        );
    }
}
