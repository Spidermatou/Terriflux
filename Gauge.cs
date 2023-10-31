using System;
using System.Collections.Generic;
using Godot;

abstract class Gauge : IGaugeObserver
{
  private double value = 100;
  private List<IGaugeObserver> observers = new List<IGaugeObserver>();

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
