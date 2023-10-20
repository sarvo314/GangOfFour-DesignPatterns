using System;
namespace DesignPatterns
{
    public interface Command
    {
        public void execute();
        public void undo();
    }

    class Light
    {

        public void on()
        {
            Console.WriteLine("Light is on");
        }

        public void off()
        {
            Console.WriteLine("Light is off");
        }


    }

    class LightOnCommand : Command
    {
        Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void execute()
        {
            light.on();
        }
        public void undo()
        {
            light.off();
        }
    }


    class LightOffCommand : Command
    {
        Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void execute()
        {
            light.off();
        }

        public void undo()
        {
            light.on();
        }
    }

    public class Stereo
    {
        public void on()
        {
            Console.WriteLine("Stereo is on");
        }

        public void off()
        {
            Console.WriteLine("Stereo is off");
        }

        public void setVolume(int volume)
        {
            Console.WriteLine("Stereo volume set to " + volume);
        }

    }
    public class StereoOffCommand : Command
    {
        Stereo stereo;

        public StereoOffCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void execute()
        {
            stereo.off();
        }
    }
    public class StereoOnCommand : Command
    {
        Stereo stereo;

        public StereoOnCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void execute()
        {
            stereo.on();
        }
    }

    public class SimpleRemoteControl
    {
        Command slot;

        public void setCommand(Command command)
        {
            slot = command;
        }

        public void buttonWasPressed()
        {
            slot.execute();
        }
    }

    public class CommandPattern
    {
        public CommandPattern()
        {

            // Console.Write("eh");
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("hsd");
            SimpleRemoteControl remote = new SimpleRemoteControl();
            Light light = new Light();
            Stereo stereo = new Stereo();

            remote.setCommand(new LightOnCommand(light));
            remote.buttonWasPressed();

            remote.setCommand(new StereoOnCommand(stereo));
            remote.buttonWasPressed();
            CommandPattern cp = new CommandPattern();

        }
    }
}

