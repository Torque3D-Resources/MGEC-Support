//------------------------------------------------------
// Copyright 2103-2007, Hall Of Worlds, LLC.
//------------------------------------------------------

datablock WheeledVehicleData( wheeledRandomPathFollower : DefaultCar ) 
{
   category = "gpgt";
   maxSteeringAngle = 0.785;  // Better than original car
   maxAISpeed = 0.8;
   moveTol    = 5.0;
};

package exercisePackage_103
{

// 1 - Create a path for our AIWheeledVehicle to follow.
// 2 - Spawn an AI wheeled vehicle using our datablock.
// 3 - Assign the path to our wheeled bot.
// 4 - Initialize the AI wheeled vehicle to start at path node zero.
// 5 - Start the AI wheeled vehicle moving towards the initial node.
function startexercise103()
{     
   // 1
   exerciseCenter.createSimplePath( "testPath" , 25 );
   
   // 2
   %theBot = AIWheeledVehicle::spawn( exerciseCenter.getTransform() , wheeledRandomPathFollower );
   
   // 3 
   %theBot.assignPath( testPath );
   
   // 4
   %theBot.currentPathNodeNum = 0;
   
   // 5
   %pathNode = %theBot.myPath.getObject( %theBot.currentPathNodeNum );
   %theBot.setMoveDestination( %pathNode.getTransform() , false );

   %pathNode.visibleMarker.setSkinName("green");
}

function wheeledRandomPathFollower::onAdd( %DB, %theBot )
{
	%callerDBName = %DB.getName();
	echo(%callerDBName @ "::onAdd( " @ %DB.getID() @" , " @ %theBot @ " )");
   Parent::onAdd( %DB , %theBot );
	%theBot.setMoveSpeed( %DB.maxAISpeed );
	%theBot.setMoveTolerance( %DB.moveTol );
}

// 1 - Randomly select a new target node.
// 2 - Start the AI wheeled vehicle moving towards the next node.
function wheeledRandomPathFollower::onReachDestination( %DB , %theBot )
{
   %pathNode = %theBot.myPath.getObject( %theBot.currentPathNodeNum );
   %pathNode.visibleMarker.setSkinName("red");

   // 1
   %theBot.currentPathNodeNum = getRandom( 0 , 7 );
   
   // 2
   %pathNode = %theBot.myPath.getObject( %theBot.currentPathNodeNum );
   %theBot.setMoveDestination( %pathNode.getTransform() , false );

   %pathNode.visibleMarker.setSkinName("green");
}
};