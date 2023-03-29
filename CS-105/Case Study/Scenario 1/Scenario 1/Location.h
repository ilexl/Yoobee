#include <string>
#pragma once
/// <summary>
/// Latitude and longitude location for ocean navigation
/// </summary>
class Location
{
	int degrees;
	float minutes; // Seconds not required as minutes take decimals for seconds
	char direction; // Compass ('N','E','S','W')

	void getDegrees();
	void getMinutes();
	void getDirection();

public:
	/// <summary>
	/// Latitude or longitude location for ocean navigation<para></para>
	/// Sets all values to 0
	/// </summary>
	Location();

	/// <summary>
	/// <para>Obtains a location value from the user in</para>
	/// <para>Degrees   (latitude between 0 - 180) </para>
	/// <para>Minutes   (longitude between 0 to 60) </para>
	/// <para>Direction (compass N, E, S, W) </para>
	/// </summary>
	void getPos();

	/// <summary>
	/// Display the location latitude and longitude in (###°##’ D) format
	/// </summary>
	void display();
};

