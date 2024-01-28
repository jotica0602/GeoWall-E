regularHexagon(p,m) =
    let
        point p2;
        l1 = line(p,p2);
        c1 = circle(p,m);
        i1,i2,_ = intersect(l1,c1);
        c2 = circle(i1,m);
        c3 = circle(i2,m);
        i3,i4,_ = intersect(c2,c1);
        i5,i6,_ = intersect(c3,c1);
color red;
        draw {segment (i1,i3),segment(i3,i6),segment(i6,i2),segment(i2,i5),segment(i5,i4),segment(i4,i1)};
restore;
    in {i1,i3,i6,i2,i5,i4};


mediatrix(p1, p2) = 
    let
        l1 = line(p1, p2);
        m = measure (p1, p2);
        c1 = circle (p1, m);
        c2 = circle (p2, m);
        i1,i2,_ = intersect(c1, c2);
    in line(i1,i2);

hexagonalStar(p,m) =
   let 
       v1,v2,v3,v4,v5,v6,_ = regularHexagon(p,m);
       l1 = mediatrix(v1,v2);
       l2 = mediatrix(v2,v3);
       l3 = mediatrix(v3,v4);
       i1,_ = intersect(l1,line(v3,v4));
       i2,_ = intersect(l1,line(v3,v2));
       i3,_ = intersect(l2,line(v1,v2));
       i4,_ = intersect(l2,line(v1,v6));
       i5,_ = intersect(l3,line(v2,v3));
       i6,_ = intersect(l3,line(v2,v1));
       
   in {v1,i2,v2,i3,v3,i5,v4,i1,v5,i4,v6,i6};

centro = point(300,300);
draw centro "Centro";
hexagonalStar(centro,150);