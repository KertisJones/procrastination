#pragma strict
  
 function Start () {
    bgColorShifter();
 }
  
 function bgColorShifter()
 {
     var bgColor : Color; 
     
     while (true)
     { 
         bgColor = Color(Random.value, Random.value, Random.value, 1.0);
         
         var t: float = 0f;
         var currentColor = Camera.main.backgroundColor;
        
         while( t < 1.0 )
         {
             Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t );
             yield;
             t += Time.deltaTime;

             //Camera.current.backgroundColor = Camera.main.backgroundColor;
         }
     }
 }