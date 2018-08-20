	// List of all the public variables.
	var xMax = 19;		// The map size X and Y.
	var yMax = 16;
	var myArrayNum = 0;  // Decide what map will be selected.
	var wolves = [];	 // Array for the wolves.
	var player = 0;		// Players current position to be set when printing the map.
	var playerPosY = 0;		// Players Y position set when printing the map.
	var playerPosX = 0;		// Players X position.
	var wolfInterval; // The variable for wolves movement timer.
	var timeTick;		// The variable for the Timer count.
	var moveCount = 0;
	var Timer = 0;
	
	function Map()    // Prints the map.
	{
		
			var popup = document.createElement('div');  // Adds the pop up window into the same div as the map.
			popup.id = 'screen';
			document.getElementById("mapzone").appendChild(popup);
	
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
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'w')
					{
						boxDiv.classList.add("w");
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'b')
					{
						boxDiv.classList.add("b");
					}			
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'g')
					{
						boxDiv.classList.add("g");
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'p')
					{
						boxDiv.classList.add("p");
						player = boxDiv;
						playerPosY = y;
						playerPosX = x;
					}	
					else if(mapsArray[myArrayNum].mapGrid[y][x] == 'h')
					{
						boxDiv.classList.add("h");
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
			document.getElementById("mapzone").appendChild(rowDiv);
		}
	}

	function FinishMap()   // When all chickens made it to the dens.
	{
		var goal = document.getElementsByClassName("box g");
		var goalSet = document.getElementsByClassName("box g c");
		
		if(goal.length == 0)  // A check to see if the 0 dens or keep going
		{
			return false;
		}
			
		for(var i = 0; i < goal.length; i++)  // Loop to check what dens got a chicken in them.
		{    
			if (goal[i].classList.contains("c"))
			{
				continue;
			}
			else
			{
				return false;
			}
		}
	
		if(goalSet.length == goal.length)  // when the den number = the den with chickens in prints the win message.
		{
			document.getElementById("screen").innerHTML = "<h3>You caught all the chickens</h3>";
			clearInterval(wolfInterval);   // Stop the wolves from moving. 
			clearInterval(timeTick);	// Stop the timer.
			document.removeEventListener("keydown", Moving) // Stop the player from moving.
		}
		return true;        
	}
	
	function DeathByPlayer()  // If player walks into a wolf.
	{
		document.getElementById("screen").innerHTML = "<h3>You walked into your death</h3>";
	    player.classList.remove("p");
	    player.classList.remove("p2");
		player.classList.remove("p3");
		player.classList.remove("p4");
		player.classList.remove("d");
		clearInterval(wolfInterval);
		clearInterval(timeTick);
		document.removeEventListener("keydown", Moving)
    }	
	
	function DeathByWolf()    // If a wolf walk into the player.
	{
		document.getElementById("screen").innerHTML = "<h3>Wolf killed you</h3>";
		nextWolf.classList.remove("p");
		nextWolf.classList.remove("p2");
		nextWolf.classList.remove("p3");
		nextWolf.classList.remove("p4");
		clearInterval(wolfInterval);
		clearInterval(timeTick);
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
		wolf.classList.remove("h");
		wolf.classList.remove("hu");
		wolf.classList.remove("hd");
		wolf.classList.remove("hr");
		wolf.classList.remove("hl");
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
	
	function MoveChicken()	// Moves the chicken.
	{
		nextSpot.classList.remove("c");   
		nextNextSpot.classList.add("c");
	}
	
	function Move()		// Moves the player up.
	{  
		if(!nextSpot){}  // Checking so player don't go out of the map.
		else if(nextSpot.classList.contains("w")){}
		else if(nextSpot.classList.contains("h") || nextSpot.classList.contains("hu") || nextSpot.classList.contains("hd") || nextSpot.classList.contains("hr") || nextSpot.classList.contains("hl"))
		{
			DeathByPlayer();
		}
		else if(nextSpot.classList.contains("c"))
		{
			if(!nextNextSpot){}   // Check diffrent things like edge of the map not turning object to null.
			else if(nextNextSpot.classList.contains("c") || nextNextSpot.classList.contains("w") || nextNextSpot.classList.contains("h") || nextNextSpot.classList.contains("hu") || nextNextSpot.classList.contains("hd") || nextNextSpot.classList.contains("hl") || nextNextSpot.classList.contains("hr")){}
			else if(nextNextSpot.classList.contains("g"))
			{
				RemovePlayer();
				DirectionAnimation();     // Moves the player.
				MoveChicken();
				UpdatePosition();	
			}
			else if(nextSpot.classList.contains("g"))
			{
				RemovePlayer();
				nextSpot.classList.add("d");
				MoveChicken();
				UpdatePosition();
			}
			else
			{
				RemovePlayer();
				DirectionAnimation();
				MoveChicken();
				UpdatePosition();
			}  
		}
		else if(nextSpot.classList.contains("g"))
		{
			RemovePlayer();
			nextSpot.classList.add("d");
			UpdatePosition();
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
		moveCount++;
		document.getElementById("count").innerHTML = "Amount of moves: " + moveCount;
		FinishMap();
	}
	
	function TimeCount()  // Count the seconds while playing.
	{
		Timer++;
		document.getElementById("time").innerHTML = "Time: " + Timer + " seconds"; // Print the time  count on webpage.
	}
	
	function Reset()		// Reset the map.
	{
		document.getElementById("count").innerHTML = "";  // Clear the webpage of the move count and timer.
		document.getElementById("time").innerHTML = "";
		moveCount = 0;		// Reset the move count and timer to 0.
		Timer = 0;
		document.addEventListener("keydown", Moving) // Add back the listener for player movement.
		clearInterval(wolfInterval);  // Clear wolf movment and timer intervals.
		clearInterval(timeTick);
		wolves = [];  // Clear the wolves array to empty.
		document.getElementById("screen").innerHTML = "";   //Clear the pop up and game window.
		document.getElementById("mapzone").innerHTML = "";
		Map();	// Prints the new map.
		wolfInterval = setInterval(wolfDirection, 400);  // Start the wolves movement and timer again(set in milli seconds). 
		timeTick = setInterval(TimeCount, 1000);
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
	
	function WolfPositionUpdate(i) 	 // Function to update wolf position based on the roll.
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
	
	function WolfAnimation(i)	// Function to add right image based on direction.
	{
		if(roll == 1)
		{
			nextWolf.classList.add("hu");
		}
		else if(roll == 2)
		{
			nextWolf.classList.add("hl");
		}
		else if(roll == 3)
		{
			nextWolf.classList.add("hd");
		}	
		else if(roll == 4)
		{
			nextWolf.classList.add("hr");
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

function wolfRedirection(i)   // Make adjustment to the wolf direction if he can't move.
{
    roll = Math.floor(Math.random() * 4 + 1);

    if (roll == 1) {
        wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
        nextWolf = document.getElementById("y" + (wolves[i].y - 1) + "x" + wolves[i].x);
        wolfMove(i);
    }
    else if (roll == 2) {
        wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
        nextWolf = document.getElementById("y" + wolves[i].y + "x" + (wolves[i].x - 1));
        wolfMove(i);
    }
    else if (roll == 3) {
        wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
        nextWolf = document.getElementById("y" + (wolves[i].y + 1) + "x" + wolves[i].x);
        wolfMove(i);
    }
    else if (roll == 4) {
        wolf = document.getElementById("y" + (wolves[i].y) + "x" + wolves[i].x);
        nextWolf = document.getElementById("y" + wolves[i].y + "x" + (wolves[i].x + 1));
        wolfMove(i);
    }

}	
	
	function wolfMove(i)    // Moves the wolf based on its index (i).
	{
        if (!nextWolf) { wolfRedirection(i)}
        else if (nextWolf.classList.contains("w")) { wolfRedirection(i)}
        else if (nextWolf.classList.contains("c")) { wolfRedirection(i)}
        else if (nextWolf.classList.contains("hu") || nextWolf.classList.contains("hd") || nextWolf.classList.contains("hr") || nextWolf.classList.contains("hl")) { wolfRedirection(i)}
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