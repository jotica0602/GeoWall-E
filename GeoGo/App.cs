using GeoEngine;
class Interpreter
{
    static void Main()
    {
        Auto();
    }

    public static void Auto()
    {
        string input = @"
<<<<<<< HEAD
mediatrix(p1, p2) = 
    let
        l1 = line(p1, p2);
        m = measure (p1, p2);
        c1 = circle (p1, m);
        c2 = circle (p2, m);
        i1,i2,_ = intersect(c1, c2);
    in line(i1,i2);
    
regularTriangle(p,m) =
    let
        point p2;
        l1 = line(p,p2);
        c1 = circle(p,m);
        i1,i2,_ = intersect(l1,c1);
        c2 = circle(i1,m);
        c3 = circle(i2,m);
        i3,i4,_ = intersect(c2,c1);
        i5,i6,_ = intersect(c3,c1);
    in {i1,i5,i6};  
    
divideTriangle(p1,p2,p3,m1) = if (measure(p1,p2)/m1) < 15 then {} else  
   let
      draw {segment(p1,p2),segment(p2,p3),segment(p3,p1)};
      mid1,_ = intersect(mediatrix(p1,p2),line(p1,p2));
      mid2,_ = intersect(mediatrix(p2,p3),line(p2,p3));
      mid3,_ = intersect(mediatrix(p1,p3),line(p1,p3));
      a = divideTriangle(p2,mid2,mid1,m1);
      b = divideTriangle(p1,mid1,mid3,m1);
      c = divideTriangle(p3,mid2,mid3,m1);
      in {};
      
sierpinskyTriangle(p,m) = 
     let
         pu1 = point(0,0);
         pu2 = point(0,1);
         p1,p2,p3,_ = regularTriangle(p,m);
     in divideTriangle(p1,p2,p3,measure(pu1,pu2));

pu1 = point(300,0);
pu2 = point(0,0);
m = measure(pu1,pu2);

a = sierpinskyTriangle(point(450,300),m);";
=======
        point p1(250,283);
        point p2(374,283)
        ray r1(250,283);
        ray r2(645,217);
        print r1;
        print r2;
>>>>>>> development

        draw {r1,r2};

        color cyan;
        draw intersect(r1,r2);";

        #region Lexer
        string path = "prueba";
        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();
        Error.CheckErrors();
        #endregion

        #region Parser
        Scope scope = new Scope();
        ASTBuilder parser = new ASTBuilder(tokens);
        List<Node> nodes = parser.BuildNodes(scope);
        Error.CheckErrors();
        #endregion

        #region Semantic Analyzer
        foreach (Expression node in nodes.Where(node => node is Expression))
            node.CheckSemantic();

        Error.CheckErrors();
        #endregion

        #region Interpreter


        foreach (Expression node in nodes.Where(node => node is Expression))
        {
            node.Evaluate();
            System.Console.WriteLine(node);
        }
        #endregion
    }
}
