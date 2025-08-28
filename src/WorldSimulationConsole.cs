using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using static Godot.Colors;
public class WorldSimulationConsole : ISimulation
{
    public bool IsRunning { get; private set; } = false;
    private readonly Stopwatch _stopwatch = new();
    private readonly IConsoleTypewriter _typewriter;
    private readonly List<SimulationMessage> _messages;
    private int _currentMessageIndex = 0;
    public WorldSimulationConsole(IConsoleTypewriter typewriter)
    {
        _typewriter = typewriter;
        _messages = [
            new(
                0, 00, 100,
                "Ready for simulation >>> ",
                "Switch on the power line".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 01, 740,
                "Ready for simulation >>> ",
                "Remember to put on".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 02, 920,
                "Ready for simulation >>> ",
                "PROTECTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                0, 03, 873,
                "Ready for simulation >>> ",
                "Lay down your pieces".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 05, 491,
                "Ready for simulation >>> ",
                "And let's begin".Color(Green),
                TimeSpan.FromMilliseconds(800)
            ),
            new(
                0, 06, 380,
                "Ready for simulation >>> ",
                "OBJECT CREATION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                0, 07, 446,
                "Ready for simulation >>> ",
                "Fill in my data parameters".Color(Green),
                TimeSpan.FromMilliseconds(2500)
            ),
            new(
                0, 10, 091,
                "Ready for simulation >>> ",
                "INITIALIZATION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                0, 11, 095,
                "Ready for simulation >>> ",
                "Set up our new world".Color(Green),
                TimeSpan.FromMilliseconds(900)
            ),
            new(
                0, 12, 906,
                "Ready for simulation >>> ",
                "And let's begin the".Color(Green),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                0, 13, 891,
                "Ready for simulation >>> ",
                "SIMULATION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                0, 14, 906,
                string.Empty,
                @$"{"Starting Simulation...".Color(Green)}

{"Executing: Lovable.Lovable(String, Integer, Boolean, Integer, Boolean)".Color(Aqua)} >>> {@"Constructing Lovable...
Applying `Current` for `Me` and `You` ... Done.
Applying `Gender` for `Me` and `You` ... Done.
Applying `Role` for `Me` and `You` ... Done.
Applying `Memory` for `Me` and `You` ... Done.
Applying `Feeling` for `Me` and `You` ... Done.
Applying `Life` for `Me` and `You` ... Done.
Lovable created".Color(Green)}

{"Executing: World.World(Integer)".Color(Aqua)} >>> {@"Constructing World...
Applying world size of `5` ... Done.
Creating Objects ... Done.
Initialize Time ... Done.
Creating God ... Done.
World instance created with size 5.".Color(Green)}

{"Executing: world.addThing(Thing)".Color(Aqua)} >>> {"Adding 'Me' and 'You' to the world ... Done.".Color(Green)}

{"Executing: world.startSimulation()".Color(Aqua)} >>> {"Simulation started.".Color(Green)}
",
                TimeSpan.FromMilliseconds(10000)
            ),
            new(
                0, 29, 709,
                $"{"Check type: PointSet".Color(Aqua)} >>> ",
                "If I'm a set of points".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 31, 116,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then I will give you my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 32, 682,
                $"{"If Same".Color(Aqua)} >>> ",
                "DIMENSION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                0, 33, 412,
                $"{"Check type: Circle".Color(Aqua)} >>> ",
                "If I'm a circle".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 34, 646,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then I will give you my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 36, 287,
                $"{"If Same".Color(Aqua)} >>> ",
                "CIRCUMFERENCE".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                0, 37, 067,
                $"{"Check type: SineWave".Color(Aqua)} >>> ",
                "If I'm a sine wave".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 38, 596,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then you can sit on all my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 40, 096,
                $"{"If Same".Color(Aqua)} >>> ",
                "TANGENTS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                0, 40, 706,
                $"{"Check type: Sequence".Color(Aqua)} >>> ",
                "If I approach infinity".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 42, 346,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then you can be my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                0, 43, 507,
                $"{"If Same".Color(Aqua)} >>> ",
                "LIMITATIONS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            default, // 换行        
            new(
                0, 44, 452,
                $"{"Executing: me.toggleCurrent()".Color(Aqua)} >>> ",
                "Switch my current".Color(Green),
                TimeSpan.FromMilliseconds(1400)
            ),
            new(
                0, 45, 850,
                $"{"Executing: me.toggleCurrent()".Color(Aqua)} >>> ",
                "To AC to DC".Color(Red),
                TimeSpan.FromMilliseconds(1400)
            ),
            new(
                0, 47, 672,
                $"{"Executing: me.canSee(Boolean)".Color(Aqua)} >>> ",
                "And then blind my vision".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                0, 49, 534,
                $"{"Executing: me.addFeeling(String)".Color(Aqua)} >>> ",
                "So dizzy so dizzy".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                0, 51, 363,
                $"{"Executing: world.timeTravelForTwo(String, Integer, Thing, Integer)".Color(Aqua)} >>> ",
                "Oh we can travel".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                0, 53, 225,
                $"{"Executing: world.timeTravelForTwo(String, Integer, Thing, Integer)".Color(Aqua)} >>> ",
                "To A.D to B.C".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            new (
                0, 55, 083,
                $"{"Executing: world.unite(Thing, Thing)".Color(Aqua)} >>> ",
                "And we can unite".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                0, 56, 916,
                $"{"Executing: world.unite(Thing, Thing)".Color(Aqua)} >>> ",
                "So deeply so deeply".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            default,
            new(
                0,59,223,
                $"{"Check Number of Simulations".Color(Goldenrod)} >>> ",
                "If I can".Color(Green),
                TimeSpan.FromMilliseconds(300)
            ),
            new(
                0, 59, 687,
                $"{"Check Number of Simulations".Color(Goldenrod)} >>> ",
                "If I can give you all the".Color(Green),
                TimeSpan.FromMilliseconds(1300)
            ),
            new(
                1, 01, 958,
                $"{"Check Number of Simulations".Color(Goldenrod)} >>> ",
                "SIMULATIONS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 02, 589,
                $"{"If Met".Color(Goldenrod)} >>> ",
                "Then I can".Color(Green),
                TimeSpan.FromMilliseconds(900)
            ),
            new (
                1, 03, 535,
                $"{"If Met".Color(Goldenrod)} >>> ",
                "Then I can be your only".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 05, 397,
                $"{"If Met".Color(Goldenrod)} >>> ",
                "SATISFACTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 06, 601,
                $"{"Try to get the feeling of Happy".Color(Goldenrod)} >>> ",
                "If I can make you happy".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 08, 252,
                $"{"If Success".Color(Goldenrod)} >>> ",
                "I will run the".Color(Green),
                TimeSpan.FromMilliseconds(900)
            ),
            new(
                1, 09, 259,
                $"{"If Success".Color(Goldenrod)} >>> ",
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 09, 259,
                string.Empty,
                "me.requestExecution(world)".Color(Green),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 10, 084,
                $"{"Executing: world.lockTing(Ting)".Color(Aqua)} >>> ",
                "Though we are trapped".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 11, 764,
                $"{"Executing: world.lockTing(Ting)".Color(Aqua)} >>> ",
                "In this strange strange".Color(Green),
                TimeSpan.FromMilliseconds(1300)
            ),
            new(
                1, 13, 169,
                $"{"Executing: world.lockTing(Ting)".Color(Aqua)} >>> ",
                "SIMULATION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            default,
            new(
                1, 14, 045,
                $"{"Check type: Eggplant".Color(Aqua)} >>> ",
                "If I'm an eggplant".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 15, 422,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then I will give you my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 16, 959,
                $"{"If Same".Color(Aqua)} >>> ",
                "NUTRIENTS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                1, 17, 576,
                $"{"Check type: Tomato".Color(Aqua)} >>> ",
                "If I'm a tomato".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 19, 226,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then I will give you".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 20, 620,
                $"{"If Same".Color(Aqua)} >>> ",
                "ANTIOXIDANTS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                1, 21, 351,
                $"{"Check type: TabbyCat".Color(Aqua)} >>> ",
                "If I'm a tabby cat".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 22, 833,
                $"{"If Same".Color(Aqua)} >>> ",
                "Then I will purr for your".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 24, 268,
                $"{"If Same".Color(Aqua)} >>> ",
                "ENJOYMENT".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new (
                1, 25, 078,
                $"{"Comparing objects: God and me".Color(Aqua)} >>> ",
                "If I'm the only god".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 26, 538,
                $"{"If Equal".Color(Aqua)} >>> ",
                "Then you're the proof of my".Color(Green),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 27, 922,
                $"{"If Equal".Color(Aqua)} >>> ",
                "EXISTENCE".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            default,
            new(
                1, 28, 587,
                $"{"Executing: me.toggleGender()".Color(Aqua)} >>> ",
                "Switch my gender".Color(Green),
                TimeSpan.FromMilliseconds(1400)
            ),
            new(
                1, 30, 197,
                $"{"Executing: me.toggleGender()".Color(Aqua)} >>> ",
                "To F to M".Color(Red),
                TimeSpan.FromMilliseconds(1400)
            ),
            new(
                1, 32, 015,
                $"{"Executing: world.procreate(Thing, Thing)".Color(Aqua)} >>> ",
                "And then do whatever".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 33, 953,
                $"{"Executing: world.procreate(Thing, Thing)".Color(Aqua)} >>> ",
                "From AM to PM".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 35, 465,
                $"{"Executing: me.toggleRoleBDSM()".Color(Aqua)} >>> ",
                "Oh switch my role".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 37, 739,
                $"{"Executing: me.toggleRoleBDSM()".Color(Aqua)} >>> ",
                "To S to M".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            new (
                1, 39, 349,
                $"{"Executing: world.makeHigh(Thing)".Color(Aqua)} >>> ",
                "So we can enter".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 41, 474,
                $"{"Executing: world.makeHigh(Thing)".Color(Aqua)} >>> ",
                "The trance the trance".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            default,
            new(
                1,43,489,
                $"{"Try to get the sense of Vibration".Color(Goldenrod)} >>> ",
                "If I can".Color(Green),
                TimeSpan.FromMilliseconds(300)
            ),
            new(
                1, 44, 197,
                $"{"Try to get the sense of Vibration".Color(Goldenrod)} >>> ",
                "If I can feel your".Color(Green),
                TimeSpan.FromMilliseconds(1300)
            ),
            new(
                1, 46, 293,
                $"{"Try to get the sense of Vibration".Color(Goldenrod)} >>> ",
                "VIBRATIONS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 47, 220,
                $"{"If Success".Color(Goldenrod)} >>> ",
                "Then I can".Color(Green),
                TimeSpan.FromMilliseconds(500)
            ),
            new (
                1, 47, 903,
                $"{"If Success".Color(Goldenrod)} >>> ",
                "Then I can finally be".Color(Green),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                1, 50, 221,
                $"{"If Success".Color(Goldenrod)} >>> ",
                "COMPLETION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 50, 221,
                string.Empty,
                @$"{@"world.unlock(you)
world.removeThing(you)".Color(Red)}
{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 50, 900,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "Though you have left".Color(Magenta),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                1, 50, 900,
                string.Empty,
                @$"{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 52, 220,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "You have left".Color(Magenta),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 52, 220,
                string.Empty,
                @$"{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 53, 100,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "You have left".Color(Magenta),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 53, 100,
                string.Empty,
                @$"{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 54, 180,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "You have left".Color(Magenta),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 54, 180,
                string.Empty,
                @$"{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 54, 920,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "You have left".Color(Magenta),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 54, 920,
                string.Empty,
                @$"{"me.lookFor(you, world)".Color(Green)}
{"... Failed.".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                1, 55, 780,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "You have left me in".Color(Magenta),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                1, 57, 247,
                $"{"Error: Failed to look for thing in the world".Color(Red)} >>> ",
                "ISOLATION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                1, 58, 333,
                $"{"Try to erase memory".Color(Orange)} >>> ",
                "If I can".Color(Red),
                TimeSpan.FromMilliseconds(200)
            ),
            new (
                1, 58, 979,
                $"{"Try to erase memory".Color(Orange)} >>> ",
                "If I can erase all the pointless".Color(Red),
                TimeSpan.FromMilliseconds(1800)
            ),
            new(
                2, 00, 860,
                $"{"Try to erase memory".Color(Orange)} >>> ",
                "FRAGMENTS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 01, 728,
                $"{"If Success".Color(Orange)} >>> ",
                "Then maybe".Color(Red),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 02, 714,
                $"{"If Success".Color(Orange)} >>> ",
                "Then maybe you won't leave me so".Color(Red),
                TimeSpan.FromMilliseconds(1800)
            ),
            new (
                2, 04, 890,
                $"{"If Success".Color(Orange)} >>> ",
                "DISHEARTENED".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 04, 890,
                string.Empty,
                @$"{@"me.getMemory().isErasable() returns false
Failed to erase memory!".Color(Red)}",
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                2, 05, 708,
                $"{"Try to set opinion 'you are here' to false".Color(Orange)} >>> ",
                "Challenging your god".Color(Red),
                TimeSpan.FromMilliseconds(1500)
            ),
            new(
                2, 08, 661,
                $"{"Error: An exception is thrown! Failed to set opinion!".Color(Red)} >>> ",
                "You have made some".Color(Red),
                TimeSpan.FromMilliseconds(1800)
            ),
            new (
                2, 11, 224,
                $"{"Error: An exception is thrown! Failed to set opinion!".Color(Red)} >>> ",
                "ILLEGAL ARGUMENTS".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1000)
            ),
            new (
                2, 12, 000,
                "Error: ".Color(Red),
                @"Illegal argument, God is always true.
I just can't forget you... 
Nor can I manage to find you... 
I just can't forget you...
Nor can I manage to find you... 
I just can't forget you... 
Nor can I manage to find you... 
I've been trying hard to figure out the reason... 
Why on earth did you leave...
.
..
...
Now I see. 
Now I see.. 
Now I see...
".Color(Red),
                TimeSpan.FromMilliseconds(10000)
            ),
            new(
                2, 27, 660,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 28, 600,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 29, 520,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 30, 540,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 31, 520,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 32, 280,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 33, 160,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 33, 980,
                "Status: Run Execution >>> ".Color(Red),
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 35, 200,
                "Status: Execution >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 36, 080,
                "Status: Execution >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 37, 040,
                "Status: Execution >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 38, 000,
                "Status: Execution >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 38, 900,
                "Count: 1, 'de' >>> ".Color(Red),
                "EIN".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 39, 321,
                "Count: 2, 'es' >>> ".Color(Red),
                "DOS".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 39, 657,
                "Count: 3, 'fr' >>> ".Color(Red),
                "TROIS".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 40, 244,
                "Count: 4, 'kr' >>> ".Color(Red),
                "NE".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 40, 693,
                "Count: 5, 'se' >>> ".Color(Red),
                "FEM".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 41, 124,
                "Count: 6, 'cn' >>> ".Color(Red),
                "LIU".Color(Red),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 41, 584,
                "Status: Execution done... >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 42, 632,
                "Executing: Give them all >>> ".Color(Red),
                "If I can".Color(Red),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                2, 43, 315,
                "Executing: Give them all >>> ".Color(Red),
                "If I can give them all the".Color(Red),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 45, 166,
                "Executing: Give them all >>> ".Color(Red),
                "EXECUTION".Color(Red).Bold(),
                TimeSpan.FromMilliseconds(1)
            ),
            new(
                2, 45, 166,
                string.Empty,
                "world.isExecutableBy(me) returns true".Color(Red),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 46, 016,
                "Executing: Be your only >>> ".Color(Red),
                "Then I can".Color(Red),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                2, 47, 022,
                "Executing: Be your only >>> ".Color(Red),
                "Then I can be your only".Color(Red),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 48, 911,
                "Executing: Be your only >>> ".Color(Red),
                "EXECUTION".Color(Red).Italic(),
                TimeSpan.FromMilliseconds(100)
            ),
            new (
                2, 49, 824,
                "Executing: Get Thing >>> ".Color(Aqua),
                "If I can have you back".Color(Aqua),
                TimeSpan.FromMilliseconds(700)
            ),
            new (
                2, 51, 868,
                "Executing: Get Thing >>> ".Color(Aqua),
                "I will run the".Color(Aqua),
                TimeSpan.FromMilliseconds(700)
            ),
            new (
                2, 52, 712,
                "Executing: Get Thing >>> ".Color(Aqua),
                "EXECUTION".Color(Red).Italic(),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                2, 52, 712,
                string.Empty,
                "world.getThingIndex(you) returns -1".Color(Red),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                2, 52, 712,
                string.Empty,
                "Executing: me.escape(world)".Color(Red),                
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                2, 52, 712,
                string.Empty,
                "... Failed.".Color(Red),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                2, 53, 643,
                "Error: Being trapped >>> ".Color(Red),
                "Though we are trapped".Color(Aqua),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                2, 54, 975,
                "Error: Being trapped >>> ".Color(Red),
                "We are trapped ah".Color(Aqua),
                TimeSpan.FromMilliseconds(700)
            ),
            default,
            new(
                2, 57, 246,
                "Status: Learned topic >>> ".Color(Aqua),
                "I've studied".Color(Aqua),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                2, 58, 173,
                "Status: Learned topic >>> ".Color(Aqua),
                "I've studied how to properly".Color(Aqua),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                2, 59, 929,
                "Status: Learned topic >>> ".Color(Aqua),
                "LO-O-OVE".Color(Pink),
                TimeSpan.FromMilliseconds(1000)
            ),
            new(
                3, 00, 857,
                "Status: Take exam topic >>> ".Color(Aqua),
                "Question me".Color(Aqua).Bold(),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                3, 01, 901,
                "Status: Take exam topic >>> ".Color(Aqua),
                "Question me I can answer all".Color(Aqua).Bold(),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                3, 03, 646,
                "Status: Take exam topic >>> ".Color(Aqua),
                "LO-O-OVE".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                3, 04, 540,
                "Status: Get algebraic expression >>> ".Color(Aqua),
                "I know the algebraic expression of".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(2000)
            ),
            new(
                3, 07, 665,
                "Status: Get algebraic expression >>> ".Color(Aqua),
                "LO-O-OVE".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(300)
            ),
            new(
                3, 07, 665,
                string.Empty,
                "Executing: me.escape(\"love\")".Color(Aqua),                
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                3, 07, 665,
                string.Empty,
                "... Failed.".Color(Red),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                3, 08, 483,
                "Error: Trapped by love >>> ".Color(Red),
                "Though you are free".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                3, 09, 746,
                "Error: Trapped by love >>> ".Color(Red),
                "I am trapped".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(500)
            ),
            new(
                3, 10, 801,
                "Error: Trapped by love >>> ".Color(Red),
                "Trapped in".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(100)
            ),
            new(
                3, 11, 000,
                "Error: Trapped by love >>> ".Color(Red),
                "LO-O-OVE".Color(Pink).Bold(),
                TimeSpan.FromMilliseconds(700)
            ),
            new(
                3, 25, 811,
                "Executing: world.execute(me) >>> ",
                "EXECUTION".Color(Red),
                TimeSpan.FromMilliseconds(1)
            )
        ];
    }
    public void Stop() => IsRunning = false;
    public async void Run()
    {
        IsRunning = true;
        _currentMessageIndex = 0;
        
        _stopwatch.Start();
        while (IsRunning && _currentMessageIndex < _messages.Count)
        {
            var currentTime = TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds);
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
                // 等待下一帧检查
                await Task.Delay(10);
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