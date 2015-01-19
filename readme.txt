my first commit

this project is a language parser,a very simple language.
u can write code like this:
1:
 i=2+3*3/(4-2)
2:
 i=1;
 if(i)
 {
 	i=2;
 } 

3: 
 i=4;
 sum=0;
 while(i)
{
 i--;
 sum = sum +i;
}
sum;

4:
fun name(i,j){ i+j;}
name(2,3);
name(3,6);

5:
kk = fun name(i,j,k){i+j+k;}
kk(1,2,3);
kk(1,2,6);

6:[call c# static function]
invoke("System.Console","WriteLine","helloworld");

or[some shortcuts in this parser]
invoke("print","helloworld");

invoke("time");

7:
ToBeContinue