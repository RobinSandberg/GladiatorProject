	// List of all the public variables.
	var xMax = 19;		// The map size X and Y.
	var yMax = 16;
	var myArrayNum = 0;  // Decide what map will be selected.
	var wolves = [];	 // Array for the wolves.
	var player = 0;		// Players current position to be set when printing the map.
	var playerPosY = 0;		// Players Y position set when printing the map.
	var playerPosX = 0;		// Players X position.
	var wolfInterval;	 // The variable for wolves movement timer.
	var timeTick;	// The variable for the Timer count.
	var score = 0;
	var scoreTick;  // The variable for the Score interval timer.
	var Timer = 0;
	var survive;    // The Variable that run the interval for function survivalStart.
	
	function Map()    // Prints the map.
	{
		
			var popup = document.createElement('div');  // Adds the pop up window into the same div as the map.
			popup.id = 'screen';
			document.getElementById("survivezone").appendChild(popup);
	
		for (var y = 0; y < yMax; y++)	//Make the div for each row.
		{
			var rowDiv = document.createElement('div');
			rowDiv.id = 'row'+'y'+y;
			rowDiv.className = 'myrow';
	
			for (var x = 0; x < xMax; x++)
			{
				// Create the inner div before appending to the body
				var boxDiv = document.createElement('div');
				boxDiv.id = 'y'+y+'x'+x;
				boxDiv.className = 'box';
					if(mapsArray[myArrayNum].mapGrid[y][x] == 'c')
					{   
						boxDiv.classList.add("c");
					}
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'o')  // Lava (Walls)
					{
						boxDiv.classList.add("o");
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'x')  // Horizontal bridges
					{
						boxDiv.classList.add("x");
					}			
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'y')  // Vertical bridges
					{
						boxDiv.classList.add("y");
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'p')  // Player
					{
						boxDiv.classList.add("p");
						player = boxDiv;
						playerPosY = y;
						playerPosX = x;
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'r')  // Wolves
					{
						boxDiv.classList.add("r");
						var wolf = boxDiv;
						var wolfPosY = y;
						var wolfPosX = x;
						wolves.push({	// Making an object for  the array so each wolf get saved.
							div: boxDiv,
							y: y,
							x: x
						});
					}	
				// The variable rowDiv is still good... Just append to it.
				rowDiv.appendChild(boxDiv);
			}
			// Then append the rowDiv with all it's boxDiv's onto the body
			document.getElementById("survivezone").appendChild(rowDiv);
		}
	}

	function SurvivalStart() // Increase the speed at set scores and finish the game at set time.
	{
		if(Timer <= 0)    // Start the score count as soon as timer hit 0 then start to count up again.
		{
			clearInterval(timeTick);
			Timer = 1;
			timeTick = setInterval(TimeCount, 1000);
			scoreTick = setInterval(ScoreCount, 100);
			return false;
		}
		
		if (score >= 270)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 800);
		}
		if (score >= 550)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 600);
		}
		if (score >= 1200)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 400);
		}
		if (score >= 1800)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 200);
		}
		if (score >= 2400)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 100);
		}
		if (score >= 3200)
		{	
			clearInterval(wolfInterval);
			wolfInterval = setInterval(wolfDirection, 50);
		}
		if(Timer >= 60)  
		{ 
			score += 2000;
			document.getElementById("screen").innerHTML = "<h3>You survived long enough and earned 2000 extra points!</h3>"+ "Total score: "+score;
			clearInterval(wolfInterval);   // Stop the wolves from moving. 
			clearInterval(timeTick);	// Stop the timer.
			clearInterval(scoreTick);
			clearInterval(survive);
			document.removeEventListener("keydown", Moving) // Stop the player from moving.
		}
		
	}
	
	function DeathByPlayer()  // If player walks into a wolf.
	{
		document.getElementById("screen").innerHTML = "<h3>You walked into death</h3>"+ "Total score: "+score;
	    player.classList.remove("p");
	    player.classList.remove("p2");
		player.classList.remove("p3");
		player.classList.remove("p4");
		player.classList.remove("d");
		clearInterval(wolfInterval);
		clearInterval(timeTick);
		clearInterval(scoreTick);
		clearInterval(survive);
		document.removeEventListener("keydown", Moving)
    }	
	
	function DeathByWolf()    // If a wolf walk into the player.
	{
		document.getElementById("screen").innerHTML = "<h3>Death catched you</h3>"+ "Total score: "+score;
		nextWolf.classList.remove("p");
		nextWolf.classList.remove("p2");
		nextWolf.classList.remove("p3");
		nextWolf.classList.remove("p4");
		clearInterval(wolfInterval);
		clearInterval(timeTick);
		clearInterval(scoreTick);
		clearInterval(survive);
		document.removeEventListener("keydown", Moving)
	}
	
	function RemovePlayer()   // Function for removing the player classes.
	{
		player.classList.remove("p");
		player.classList.remove("p2");
	    player.classList.remove("p3");
	    player.classList.remove("p4");
		player.classList.remove("d");
	}
	
	function RemoveWolf()	 // Function for removing the wolf classes.
	{
		wolf.classList.remove("r");
		wolf.classList.remove("ru");
		wolf.classList.remove("rd");
		wolf.classList.remove("rr");
		wolf.classList.remove("rl");
	}
	
	function UpdatePosition()  // Function to update what direction the player moved.
	{
		if(event.key == "ArrowUp")
		{
			playerPosY--; 
		}
		else if(event.key == "ArrowDown")
		{
			playerPosY++; 
		}
		else if(event.key == "ArrowLeft")
		{
			playerPosX--; 
		}
		else if(event.key == "ArrowRight")
		{
			playerPosX++; 
		}
	}	
	
	function DirectionAnimation()  // Function to change what Image used when moving.
	{
		if(event.key == "ArrowUp")
		{
			nextSpot.classList.add("p");
		}
		else if(event.key == "ArrowDown")
		{
			nextSpot.classList.add("p2");
		}
		else if(event.key == "ArrowLeft")
		{
			nextSpot.classList.add("p3");
		}
		else if(event.key == "ArrowRight")
		{
			nextSpot.classList.add("p4");
		}
	}
	
	function Move()		// Moves the player up.
	{  
		if(!nextSpot){}  // Checking so player don't go out of the map.
		else if(nextSpot.classList.contains("o")){}
		else if(nextSpot.classList.contains("r") || nextSpot.classList.contains("ru") || nextSpot.classList.contains("rd") || nextSpot.classList.contains("rr") || nextSpot.classList.contains("rl"))
		{
			DeathByPlayer();
		}
		else
		{
			RemovePlayer();
			DirectionAnimation();
			UpdatePosition();
		}	
	}
	
	function Moving(event)	// Event for pushing the arrow keys.
	{
		event.preventDefault();  // Prevents the arrow keys from scrolling the webpage.
	    if(event.key == "ArrowLeft")
	    {
			player = document.getElementById("y" + playerPosY + "x" + playerPosX);
			nextSpot = document.getElementById("y" + playerPosY + "x" + (playerPosX-1));
			nextNextSpot = document.getElementById("y" + playerPosY + "x" + (playerPosX-2));
			Move();
		}
		else if(event.key == "ArrowUp")
	    {
			player = document.getElementById("y" + playerPosY + "x" + playerPosX);   // The players location 
			nextSpot = document.getElementById("y" + (playerPosY-1) + "x" + playerPosX);  // Checking the location the player trying to move to
			nextNextSpot = document.getElementById("y" + (playerPosY-2) + "x" + playerPosX);  // Checking 2 locations forward
			Move();
		}
		else if(event.key == "ArrowDown")
		{
			player = document.getElementById("y" + playerPosY + "x" + playerPosX);
			nextSpot = document.getElementById("y" + (playerPosY+1) + "x" + playerPosX);
			nextNextSpot = document.getElementById("y" + (playerPosY+2) + "x" + playerPosX);
			Move();
		}
		else if(event.key == "ArrowRight")
		{
			player = document.getElementById("y" + playerPosY + "x" + playerPosX);
			nextSpot = document.getElementById("y" + playerPosY + "x" + (playerPosX+1));
			nextNextSpot = document.getElementById("y" + playerPosY + "x" + (playerPosX+2));
			Move();
		}
		score += 3;   // Adds 3 points every time you move.
	}
	
	function TimeCount()  // Count the seconds while playing.
	{
		Timer++;
		document.getElementById("time").innerHTML = "Time: " + Timer + " seconds"; // Print the time  count on webpage.
	}
	
	function CountDown()  // Countdown for starting the score.
	{
		Timer--;
		document.getElementById("time").innerHTML = "Time: " + Timer + " seconds"; // Print the time  count on webpage.
	}
	
	function ScoreCount()  // Give 5 points for every scoreTick interval.
	{
		score +=5;
		document.getElementById("count").innerHTML = "Score: " + score + " Points"; // Print the time  count on webpage.
	}
	
	function Reset()		// Reset the map.
	{
		document.getElementById("count").innerHTML = "";  // Clear the webpage of the move count and timer.
		document.getElementById("time").innerHTML = "";
		document.addEventListener("keydown", Moving) // Add back the listener for player movement.
		clearInterval(wolfInterval);  // Clear wolf movment and timer intervals.
		clearInterval(timeTick);
		clearInterval(scoreTick);
		clearInterval(survive);
		score = 0;		// Reset the move count and timer to 0.
		Timer = 5;
		wolves = [];  // Clear the wolves array to empty.
		document.getElementById("screen").innerHTML = "";   //Clear the pop up and game window.
		document.getElementById("survivezone").innerHTML = "";
		Map();		// Prints the new map.
		wolfInterval = setInterval(wolfDirection, 1000);  // Start the wolves movement and timer again(set in milli seconds). 
		timeTick = setInterval(CountDown, 1000);
		survive = setInterval(SurvivalStart, 5000);
	}
	var resetGame = document.getElementById("resetMap");
	resetGame.addEventListener("click" , Reset);
	
	function NextMap()				// Button for next map.
	{
		if(myArrayNum == mapsArray.length -1){}
		else
		{
			myArrayNum ++;
			Reset();
		}
	}
	var nextMap = document.getElementById("nextMap");	// Getting the button by Id.
	nextMap.addEventListener("click" , NextMap);		// webpage Listen for when the button get clicked.
	
	function PrevMap()
	{
		if(myArrayNum < 1){}
		else
		{
			myArrayNum --;
			Reset();
		}
	}
    var prevMap = document.getElementById("prevMap");
	prevMap.addEventListener("click" , PrevMap);
	
	function MapButtons()  // The buttons creation in drop down selection.
	{
		for(var i = 0; i < mapsArray.length;i++)   // A loop checking how many maps there is to make buttons for.
		{
			var btn = document.createElement("BUTTON");   // create the button
			btn.id = (i+1);   // Give button an Id
			btn.onclick = MapSelection;    // Adding a onclick function to the buttons
			var text = document.createTextNode("Map " + (i + 1));   // Add text to the button.
			btn.appendChild(text);   // print the text on the button.
			
			document.getElementById("maps").appendChild(btn);  // Print the button.
		}	
	}	
	
	function MapSelection(e)  // The function for clicking the map buttons. (e) = event.
	{
		var id = e.target.id;
		myArrayNum = id-1;
		Reset();
	}
	
	function topFunction()  // Scroll the page back to the top.
	{
		document.body.scrollTop = 0;
		document.documentElement.scrollTop = 0;
	}
	
	function WolfPositionUpdate(i)  // Function to update wolf position based on the roll.
	{
		if(roll == 1)
		{
			wolves[i].y--;
		}
		else if(roll == 2)
		{
			wolves[i].x--;
		}
		else if(roll == 3)
		{
			wolves[i].y++;
		}	
		else if(roll == 4)
		{
			wolves[i].x++;
		}
	}
	
	function WolfAnimation(i)  // Function to add right image based on direction.
	{
		if(roll == 1)
		{
			nextWolf.classList.add("ru");
		}
		else if(roll == 2)
		{
			nextWolf.classList.add("rl");
		}
		else if(roll == 3)
		{
			nextWolf.classList.add("rd");
		}	
		else if(roll == 4)
		{
			nextWolf.classList.add("rr");
		}
	}
	
	var roll;
	
	function wolfDirection()   // Decides the direction for the wolves.
	{
		for(var i = 0; i < wolves.length; i++) {    // Checking the index based on the wolves array.
			roll = Math.floor(Math.random() * 4 + 1);  // rolls a random number between 1 and 4.
		
			if(roll == 1)
			{
				wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
				nextWolf = document.getElementById("y" + (wolves[i].y-1) + "x" + wolves[i].x);
				wolfMove(i);
			}
			else if(roll == 2)
			{
				wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
				nextWolf = document.getElementById("y" + wolves[i].y + "x" + (wolves[i].x-1));
				wolfMove(i);
			}
			else if(roll == 3)
			{
				wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
				nextWolf = document.getElementById("y" + (wolves[i].y+1) + "x" + wolves[i].x);
				wolfMove(i);
			}
			else if(roll == 4)
			{
				wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
				nextWolf = document.getElementById("y" + wolves[i].y + "x" + (wolves[i].x+1));
				wolfMove(i);
			}
		}
	}	
	
	function wolfMove(i)    // Moves the wolf based on its index (i).
	{
		if (!nextWolf){}
		else if(nextWolf.classList.contains("o")){}
		else if(nextWolf.classList.contains("ru") || nextWolf.classList.contains("rd") || nextWolf.classList.contains("rr") || nextWolf.classList.contains("rl")){}
		else if(nextWolf.classList.contains("p") || nextWolf.classList.contains("p2") || nextWolf.classList.contains("p3") || nextWolf.classList.contains("p4"))
		{
			DeathByWolf();
		}
		else
		{
			RemoveWolf();
			WolfAnimation(i);
			WolfPositionUpdate(i);
		}	
	}
	
	MapButtons();  // Call the creation of the map buttons when loading the page.
	Reset();   // Call the reset button when the full script is loaded.