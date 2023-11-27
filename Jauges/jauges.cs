using Godot;
using System;

public partial class jauges : TextureProgressBar, IGaugeObserver
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Value = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public double getValue(){
	return this.value;
  }

  public void setValue(double newVal){
	this.value = newVal;
	notifyRateAll(newVal);
  }
  public void addObserver(IGaugeObserver observer){
	if (!observers.Contains(observer)) {
	  observers.add(observer);
	}
  }
  public void removeObserver(IGaugeObserver observer){
	if(observers.Contains(observer)){
		observers.remove(observer);
	}
  }
  public void notifyRateAll(double xVal)
  {
	foreach (IGaugeObserver go in observers){
	  go.update(xVal);
	}
  }
}
