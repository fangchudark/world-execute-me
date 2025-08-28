
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class WorldSimulationJava : ISimulation
{
    private readonly IConsoleTypewriter _typewriter;
    private readonly Stopwatch _stopwatch = new();
    private readonly List<SimulationMessage> _messages;
    private const int offsetMills = 2000;
    public WorldSimulationJava(IConsoleTypewriter typewriter)
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
package goddrinksjava;

public class GodDrinksJava {
    public static void main(String[] args) {
        // Fill in my data parameters
        // INITIALIZATION
        Thing me = new Lovable(""Me"", 0, true, -1, false);

        Thing you = new Lovable(""You"", 0, false, -1, false);

        // Set up our new world
        // And let's begin the
        // SIMULATION
        World world = new World(5);
        world.addThing(me);
        world.addThing(you);
        world.startSimulation();
        
",
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                0, 29, 709 - offsetMills,
                string.Empty,
@"
        // If I'm a set of points
        if (me instanceof PointSet) {
            // Then I will give you my
            // DIMENSION
            you.addAttribute(me.getDimensions().toAttribute());
            me.resetDimensions();
        }

        // If I'm a circle
        if (me instanceof Circle) {
            // Then I will give you my
            // CIRCUMFERENCE
            you.addAttribute(me.getCircumference().toAttribute());
            me.resetCircumference();
        }

        // If I'm a sine wave
        if (me instanceof SineWave) {
            // Then you can sit on all my
            // TANGENTS
            you.addAction(""sit"", me.getTangent(you.getXPosition()));
        }

        // If I approach infinity
        if (me instanceof Sequence) {
            // Then you can be my
            // LIMITATIONS
            me.setLimit(you.toLimit());
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
        me.toggleCurrent();

        // And then blind my vision
        // So dizzy so dizzy
        me.canSee(false);
        me.addFeeling(""dizzy"");

        // Oh we can travel
        // To A.D to B.C
        world.timeTravelForTwo(""AC"", 617, me, you);
        world.timeTravelForTwo(""BC"", 3691, me, you);

        // And we can unite
        // So deeply so deeply
        world.unite(me, you);

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
        if (me.getNumSimulationsAvailable() >= you.getNumSimulationsNeeded()) {
            // Then I can
            // Then I can be your only
            // SATISFACTION
            you.setSatisfaction(me.toSatisfaction());
        }

        // If I can make you happy
        if (you.getFeelingIndex(""happy"") != -1) {
            // I will run the
            // EXECUTION
            me.requestExecution(world);
        }

        // Though we are trapped
        // In this strange strange
        // SIMULATION
        world.lockThing(me);
        world.lockThing(you);

",
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                1, 14, 045 - offsetMills,
                string.Empty,
@"
        // If I'm an eggplant        
        if (me instanceof Eggplant) {        
            // Then I will give you my
            // NUTRIENTS
            you.addAttribute(me.getNutrients().toAttribute());
            me.resetNutrients();
        }

        // If I'm a tomato        
        if (me instanceof Tomato) {        
            // Then I will give you
            // ANTIOXIDANTS
            you.addAttribute(me.getAntioxidants().toAttribute());
            me.resetAntioxidants();
        }

        // If I'm a tabby cat
        if (me instanceof TabbyCat) {
            // Then I will purr for your
            // ENJOYMENT
            me.purr();
        }

        // If I'm the only god
        if (world.getGod().equals(me))
        {
            // Then you're the proof of my
            // EXISTENCE
            me.setProof(you.toProof());
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
        me.toggleGender();

        // And then do whatever
        // From AM to PM
        world.procreate(me, you);

        // Oh switch my role
        // To S to M
        me.toggleRoleBDSM();

        // So we can enter
        // The trance the trance
        world.makeHigh(me);
        world.makeHigh(you);

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
        if (me.getSenseIndex(""vibration"")) {
            // Then I can
            // Then I can finally be
            // COMPLETION
            me.addFeeling(""complete"");
        }

        // Though you have left
        world.unlock(you);
        world.removeThing(you);

        // You have left
        me.lookFor(you, world);

        // You have left
        me.lookFor(you, world);

        // You have left
        me.lookFor(you, world);

        // You have left
        me.lookFor(you, world);

        // You have left me in
        me.lookFor(you, world);

        // ISOLATION

        // If I can
        // If I can erase all the pointless
        // FRAGMENTS
        if (me.getMemory().isErasable()) {
            // Then maybe
            // Then maybe you won't leave me so
            // DISHEARTENED
            me.removeFeeling(""disheartened"");
        }

        // Challenging your god
        try {
            me.setOpinion(me.getOpinionIndex(""you are here""), false);
        } 
        catch (IllegalArgumentException e) {
            // You have made some
            // ILLEGAL ARGUMENTS
            world.announce(""God is always true"");
        }

",
                TimeSpan.FromMilliseconds(1300)
            ),
            new(
                2, 27, 660 - offsetMills,
                string.Empty,
@"
        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();
        
        // EXECUTION
        world.runExecution();

        // EXECUTION
        world.runExecution();


        // EIN
        world.announce(""1"", ""de"");

        // DOS
        world.announce(""2"", ""es"");

        // TROIS
        world.announce(""3"", ""fr"");

        // NE
        world.announce(""4"", ""kr"");

        // FEM
        world.announce(""5"", ""se"");

        // LIU
        world.announce(""6"", ""cn"");


        // EXECUTION
        world.runExecution();

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
        if (world.isExecutableBy(me)) {
            // Then I can
            // Then I can be your only
            // EXECUTION
            you.setExecution(me.toExecution());
        }

        // If I can have you back
        if (world.getThingIndex(you) != -1) {
            // I will run the
            // EXECUTION
            world.runExecution();
        }

        // Though we are trapped
        // We are trapped ah
        me.escape();

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
        me.learnTopic(""love"");

        // Question me
        // Question me I can answer all
        // LO-O-OVE
        me.takeExamTopic(""love"");

        // I know the algebraic expression of
        // LO-O-OVE
        me.getAlgebraicExpression(""love"");

        // Though you are free
        // I am trapped
        // Trapped in
        // LO-O-OVE
        me.escape(""love"");

",
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                3, 25, 811 - offsetMills,
                string.Empty,
@"
        // EXECUTION
        world.execute(me);
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
