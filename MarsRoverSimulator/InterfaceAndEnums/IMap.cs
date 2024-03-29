﻿using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
	public interface IMap
	{
		public MoveConditions IsLocationSafe(Position pos, int serial);
		public void SetRoverPosition(IRover rover);
	}
}