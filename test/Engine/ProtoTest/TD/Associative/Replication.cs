using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Lang;
using ProtoTestFx.TD;
using ProtoTestFx;
namespace ProtoTest.TD.Associative
{
    public class Replication
    {
        public TestFrameWork thisTest = new TestFrameWork();
        public string ReplicationRoot = "..\\..\\..\\Scripts\\TD\\Associative\\Replication\\";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("SmokeTest")]
        public void T01_Arithmatic_List_And_List_Different_Length()
        {
            //Assert.Fail("1456568 - Sprint 16 : Rev 982 : Replication does not work on operators ");
            string code = @"
list1 = { 1, 4, 7, 2};
list2 = { 5, 8, 3, 6, 7, 9 };
list3 = list1 + list2; // { 6, 12, 10, 8 }
list4 = list1 - list2; // { -4, -4, 4, -4}
list5 = list1 * list2; // { 5, 32, 21, 12 }
list6 = list2 / list1; // { 5, 2, 0, 3 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1456568 - Sprint 16 : Rev 982 : Replication does not work on operators ");
            List<Object> _list3 = new List<Object> { 6, 12, 10, 8 };
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Int64)));
            List<Object> _list4 = new List<Object> { -4, -4, 4, -4 }
    ;
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Int64)));
            List<Object> _list5 = new List<Object> { 5, 32, 21, 12 }
    ;
            Assert.IsTrue(mirror.CompareArrays("list5", _list5, typeof(System.Int64)));
            List<Object> _list6 = new List<Object> { 5, 2, 0, 3 };
            Assert.IsTrue(mirror.CompareArrays("list6", _list6, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T02_Arithmatic_List_And_List_Same_Length()
        {
            // Assert.Fail("1456568 - Sprint 16 : Rev 982 : Replication does not work on operators ");
            string code = @"
list1 = { 1, 4, 7, 2};
list2 = { 5, 8, 3, 6 };
list3 = list1 + list2; // { 6, 12, 10, 8 }
list4 = list1 - list2; // { -4, -4, 4, -4}
list5 = list1 * list2; // { 5, 32, 21, 12 }
list6 = list2 / list1; // { 5, 2, 0, 3 }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list3", new object[] { 6, 12, 10, 8 });
            thisTest.Verify("list4", new object[] { -4, -4, 4, -4 });
            thisTest.Verify("list5", new object[] { 5, 32, 21, 12 });
            thisTest.Verify("list6", new object[] { 5.0, 2.0, 0.42857142857143, 3.0 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T03_Arithmatic_Mixed()
        {
            string code = @"
list1 = { 13, 23, 42, 65, 23 };
list2 = { 12, 8, 45, 64 };
list3 = 3 * 6 + 3 * (list1 + 10) - list2 + list1 * list2 / 3 + list1 / list2; // { 128, 172, 759, 1566 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { 128, 172, 759, 1566 };
        }

        [Test]
        [Category("SmokeTest")]
        public void T04_Arithmatic_Single_List_And_Integer()
        {
            string src = @"list1 = { 1, 2, 3, 4, 5 };
a = 5;
list2 = a + list1; // { 6, 7, 8, 9, 10 }
list3 = list1 + a; // { 6, 7, 8, 9, 10 }
list4 = a - list1; // { 4, 3, 2, 1, 0 }
list5 = list1 - a; // { -4, -3, -2, -1, 0 }
list6 = a * list1; // { 5, 10, 15, 20, 25 }
list7 = list1 * a; // { 5, 10, 15, 20, 25 }
list8 = a / list1; 
list9 = list1 / a; ";
            thisTest.RunScriptSource(src);
            thisTest.Verify("list2", new object[] { 6, 7, 8, 9, 10 });
            thisTest.Verify("list3", new object[] { 6, 7, 8, 9, 10 });
            thisTest.Verify("list4", new object[] { 4, 3, 2, 1, 0 });
            thisTest.Verify("list5", new object[] { -4, -3, -2, -1, 0 });
            thisTest.Verify("list6", new object[] { 5, 10, 15, 20, 25 });
            thisTest.Verify("list7", new object[] { 5, 10, 15, 20, 25 });
            thisTest.Verify("list8", new object[] { 5.0, 2.5, 1.66666666666667, 1.25, 1.0 });
            thisTest.Verify("list9", new object[] { 0.2, 0.4, 0.6, 0.8, 1.0 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T05_Logic_List_And_List_Different_Value()
        {
            string code = @"
list1 = { 1, 8, 10, 4, 7 };
list2 = { 2, 6, 10, 3, 5, 20 };
list3 = list1 > list2; // { false, true, false, true, true }
list4 = list1 < list2;	// { true, false, false, false, false }
list5 = list1 >= list2; // { false, true, true, true, true }
list6 = list1 <= list2; // { true, false, true, false, false }
list9 = { true, false, true };
list7 = list9 && list5; // { false, false, true }
list8 = list9 || list6; // { true, false, true }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { false, true, false, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Boolean)));
            List<Object> _list4 = new List<Object> { true, false, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Boolean)));
            List<Object> _list5 = new List<Object> { false, true, true, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list5", _list5, typeof(System.Boolean)));
            List<Object> _list6 = new List<Object> { true, false, true, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list6", _list6, typeof(System.Boolean)));
            List<Object> _list7 = new List<Object> { false, false, true }
;
            Assert.IsTrue(mirror.CompareArrays("list7", _list7, typeof(System.Boolean)));
            List<Object> _list8 = new List<Object> { true, false, true };
            Assert.IsTrue(mirror.CompareArrays("list8", _list8, typeof(System.Boolean)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T06_Logic_List_And_List_Same_Length()
        {
            string code = @"
list1 = { 1, 8, 10, 4, 7 };
list2 = { 2, 6, 10, 3, 5 };
list3 = list1 > list2; // { false, true, false, true, true }
list4 = list1 < list2;	// { true, false, false, false, false }
list5 = list1 >= list2; // { false, true, true, true, true }
list6 = list1 <= list2; // { true, false, true, false, false }
list7 = list3 && list5; // { false, true, false, true, true }
list8 = list4 || list6; // { true, false, true, false, false }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { false, true, false, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Boolean)));
            List<Object> _list4 = new List<Object> { true, false, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Boolean)));
            List<Object> _list5 = new List<Object> { false, true, true, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list5", _list5, typeof(System.Boolean)));
            List<Object> _list6 = new List<Object> { true, false, true, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list6", _list6, typeof(System.Boolean)));
            List<Object> _list7 = new List<Object> { false, true, false, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list7", _list7, typeof(System.Boolean)));
            List<Object> _list8 = new List<Object> { true, false, true, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list8", _list8, typeof(System.Boolean)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T07_Logic_Mixed()
        {
            string code = @"
list1 = { 1, 5, 8, 3, 6 };
list2 = { 4, 1, 6, 3 };
list3 = (list1 > 1) && (list2 > list1) || (list2 < 5); // { true, true, false , true }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { true, true, false, true };
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Boolean)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T08_Logic_Single_List_And_Value()
        {
            string code = @"
list1 = { 1, 2, 3, 4, 5 };
a = 3;
list2 = a > list1; // { true, true, false, false, false }
list3 = list1 > a; // { false, false, false, true, true }
list4 = a >= list1; // { true, true, true, false, false }
list5 = list1 >= a; // { false, false, true, true, true }
list6 = a < list1; // { false, false, false, true, true }
list7 = list1 < a; // { true, true, false, false, false }
list8 = a <= list1; // { false, false, true, true, true }
list9 = list1 <= a; // { true, true, true, false, false }
list10 = list2 && true; // { true, true, false, false, false }
list11 = false && list2; // { false, false, false, false, false }
list12 = list2 || true; // { true, true, true, true, true }
list13 = false || list2; // { true, true, false, false, false }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { true, true, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Boolean)));
            List<Object> _list3 = new List<Object> { false, false, false, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Boolean)));
            List<Object> _list4 = new List<Object> { true, true, true, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Boolean)));
            List<Object> _list5 = new List<Object> { false, false, true, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list5", _list5, typeof(System.Boolean)));
            List<Object> _list6 = new List<Object> { false, false, false, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list6", _list6, typeof(System.Boolean)));
            List<Object> _list7 = new List<Object> { true, true, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list7", _list7, typeof(System.Boolean)));
            List<Object> _list8 = new List<Object> { false, false, true, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list8", _list8, typeof(System.Boolean)));
            List<Object> _list9 = new List<Object> { true, true, true, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list9", _list9, typeof(System.Boolean)));
            List<Object> _list10 = new List<Object> { true, true, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list10", _list10, typeof(System.Boolean)));
            List<Object> _list11 = new List<Object> { false, false, false, false, false }
;
            Assert.IsTrue(mirror.CompareArrays("list11", _list11, typeof(System.Boolean)));
            List<Object> _list12 = new List<Object> { true, true, true, true, true }
;
            Assert.IsTrue(mirror.CompareArrays("list12", _list12, typeof(System.Boolean)));
            List<Object> _list13 = new List<Object> { true, true, false, false, false };
            Assert.IsTrue(mirror.CompareArrays("list13", _list13, typeof(System.Boolean)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Replication_On_Operators_In_Range_Expr()
        {
            string code = @"
[Imperative]
{
	z5 = 4..1; // { 4, 3, 2, 1 }
	z2 = 1..8; // { 1, 2, 3, ... , 6, 7, 8 }
	z6 = z5 - z2 + 0.3;  // { 3.3, 1.3, -1.7, -2.7 }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Pass_1_single_list_of_class_type()
        {
            string code = @"
class Point_OnX
{
	x : int;
	
	constructor Point_3DCtor(p : Point_3D)
	{
		x = p.x;
	}
}
class Point_3D
{
	x : int;
	y : int;
	z : int;
	
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
}
list =  {
			Point_3D.ValueCtor(1, 2, 3), 
			Point_3D.ValueCtor(4, 5, 6),
			Point_3D.ValueCtor(7, 8, 9)
		};
		
list2 = Point_OnX.Point_3DCtor(list);
list2_0_x = list2[0].x; // 1
list2_1_x = list2[1].x; // 4
list2_2_x = list2[2].x; // 7";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list2_0_x", 1
, 0);
            thisTest.Verify("list2_1_x", 4
, 0);
            thisTest.Verify("list2_2_x", 7, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T10_Pass_2_Lists_Different_Length_2_Integers()
        {
            string code = @"
class Point_4D
{
	x : var;
	y : var;
	z : var;
	w : var;
	
	constructor ValueCtor(_x : int, _y : int, _z : int, _w : int)
	{
		x = _x;
		y = _y;
		z = _z;
		w = _w;
	}
	
	def GetValue : int()
	{
		return = x + y + z + w;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
pointList = Point_4D.ValueCtor(list1, list2, 66, 88);
pointList_0_x = pointList[0].GetValue(); // 166
pointList_5_x = pointList[5].GetValue(); // 176
pointList_9_x = pointList[9].GetValue(); // 184";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 166, 0);
            thisTest.Verify("pointList_5_x", 176, 0);
            thisTest.Verify("pointList_9_x", 184, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T11_Pass_2_lists_of_class_type_same_length_and_1_variable_of_class_type()
        {
            //Assert.Fail("Test crashes NUnit");
            string code = @"
class Point_1D
{
	x : int;
	
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
}
class Point_3D
{
	x : int;
	y : int;
	z : int;
	
	constructor Point_1DCtor(px : Point_1D, py : Point_1D, pz : Point_1D)
	{
		x = px.x;
		y = py.x;
		z = pz.x;
	}
}
p1 = {
		Point_1D.ValueCtor(1),
		Point_1D.ValueCtor(2),
		Point_1D.ValueCtor(3)
	 };
list = Point_3D.Point_1DCtor(p1, p1, Point_1D.ValueCtor(4)); 
list_0_x = list[0].x; // 1
list_1_y = list[1].y; // 2
list_2_z = list[2].z; // 4
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list_0_x", 1, 0);
            thisTest.Verify("list_1_y", 2, 0);
            thisTest.Verify("list_2_z", 4, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T12_Pass_2_Lists_Same_Length_1_Integer()
        {
            string code = @"
class Point_3D
{
	x : var;
	y : var;
	z : var;
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	def GetValue : int()
	{
		return = x + y + z;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
pointList = Point_3D.ValueCtor(list1, list2, 99);
pointList_0_x = pointList[0].GetValue(); // 111
pointList_5_x = pointList[5].GetValue(); // 121 
pointList_9_x = pointList[9].GetValue(); // 129";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 111, 0);
            thisTest.Verify("pointList_5_x", 121, 0);
            thisTest.Verify("pointList_9_x", 129, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T13_Pass_3_Lists_Different_Length()
        {
            string code = @"
class Point_3D
{
	x : var;
	y : var;
	z : var;
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	def GetValue : int()
	{
		return = x + y + z;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
list3 = { 25, 26, 27, 28, 29, 30 };
pointList = Point_3D.ValueCtor(list1, list2, list3);
pointList_0_x = pointList[0].GetValue(); // 37
pointList_3_x = pointList[3].GetValue(); // 46
pointList_5_x = pointList[5].GetValue(); // 52";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 37, 0);
            thisTest.Verify("pointList_3_x", 46, 0);
            thisTest.Verify("pointList_5_x", 52, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T14_Pass_3_Lists_Same_Length()
        {
            string code = @"
class Point_3D
{
	x : var;
	y : var;
	z : var;
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	def GetValue : int()
	{
		return = x + y + z;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
list3 = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
pointList = Point_3D.ValueCtor(list1, list2, list3);
pointList_0_x = pointList[0].GetValue(); // 33
pointList_5_x = pointList[5].GetValue(); // 48
pointList_9_x = pointList[9].GetValue(); // 60";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 33, 0);
            thisTest.Verify("pointList_5_x", 48, 0);
            thisTest.Verify("pointList_9_x", 60, 0);
        }

        [Test]
        [Category("Replication")]
        public void T15_Pass_a_3x3_and_2x4_lists()
        {
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            string code = @"
class Point_2D
{
	x : int;
	y : int;
	
	constructor ValueCtor(x1 : int, y1 : int)
	{
		x = x1;
		y = y1;
	}
	
	def GetValue()
	{
		return = x * y;
	}
}
list1 = { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
list2 = { { 1, 2, 3, 4 }, { 1, 2, 3, 4 } };
list3 = Point_2D.ValueCtor(list1, list2);
list2_0_0 = list3[0][0].GetValue(); // 1
list2_0_1 = list3[0][1].GetValue(); // 4
list2_0_2 = list3[0][2].GetValue(); // 9
list2_1_0 = list3[1][0].GetValue(); // 1
list2_1_1 = list3[1][1].GetValue(); // 4
list2_1_2 = list3[1][2].GetValue(); // 9
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list2_0_0", 1, 0);
            thisTest.Verify("list2_0_1", 4, 0);
            thisTest.Verify("list2_0_2", 9, 0);
            thisTest.Verify("list2_1_0", 1, 0);
            thisTest.Verify("list2_1_1", 4, 0);
            thisTest.Verify("list2_1_2", 9, 0);
        }

        [Test]
        [Category("Replication")]
        public void T16_Pass_a_3x3_List()
        {
            string code = @"
class Point_1D
{
	x : int;
	
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
	
	def GetValue()
	{
		return = x * x;
	}
}
list1 = { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
list2 = Point_1D.ValueCtor(list1);
list2_0_0 = list2[0][0].GetValue(); // 1
list2_1_1 = list2[1][1].GetValue(); // 4
list2_2_2 = list2[2][2].GetValue(); // 9
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            thisTest.Verify("list2_0_0", 1, 0);
            thisTest.Verify("list2_1_1", 4, 0);
            thisTest.Verify("list2_2_2", 9, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T17_Pass_ConstructorCall_Return_List()
        {
            string code = @"
class Point_1D
{
	x : int;
	
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
}
class Point_3D
{
	x : int;
	y : int;
	z : int;
	
	constructor PointOnXCtor(p : Point_1D)
	{
		x = p.x;
		y = 0;
		z = 0;
	}
	
	def GetIndexX()
	{
		return = x * x;
	}
}
list1 = { 1, 2, 3, 4, 5 };
list2 = Point_3D.PointOnXCtor(Point_1D.ValueCtor(list1));
list2_0 = list2[0].GetIndexX(); // 1
list2_3 = list2[3].GetIndexX(); // 16
list2_4 = list2[4].GetIndexX(); // 25
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list2_0", 1, 0);
            thisTest.Verify("list2_3", 16, 0);
            thisTest.Verify("list2_4", 25, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T18_Pass_ConstructorCall_Return_List_to_Function()
        {
            string code = @"
class Point_1D
{
	x : var;
	
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
}
def GetPointIndex : int(p : Point_1D)
{
	return = p.x;
}
list1 = { 1, 2, 3, 4, 5, 6 };
list2 = GetPointIndex(Point_1D.ValueCtor(list1)); // { 1, 2, 3, 4, 5, 6 }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 1, 2, 3, 4, 5, 6 }
;
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T19_Pass_FunctionCall_Return_List()
        {
            string code = @"
def foo : int(a : int)
{
	return = a * a;
}
class Point_1D
{
	x : int;
	
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
	
	def GetIndex()
	{
		return = x * x;
	}
}
list1 = { 1, 2, 3, 4, 5 };
list2 = Point_1D.ValueCtor(foo(foo(list1)));
list2_0 = list2[0].GetIndex(); // 1
list2_3 = list2[3].GetIndex(); // 65536
list2_4 = list2[4].GetIndex(); // 390625";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list2_0", 1, 0);
            thisTest.Verify("list2_3", 65536, 0);
            thisTest.Verify("list2_4", 390625, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T20_Pass_Single_List()
        {
            string code = @"
class Point_1D
{
	x : var;
	constructor ValueCtor(_x : int)
	{
		x = _x;
	}
	def GetValue : int()
	{
		return = x * x;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
pointList = Point_1D.ValueCtor(list1);
pointList_0_x = pointList[0].GetValue(); // 1
pointList_5_x = pointList[5].GetValue(); // 36
pointList_9_x = pointList[9].GetValue(); // 100";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 1, 0);
            thisTest.Verify("pointList_5_x", 36, 0);
            thisTest.Verify("pointList_9_x", 100, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T21_Pass_Single_List_2_Integer()
        {
            string code = @"
class Point_3D
{
	x : var;
	y : var;
	z : var;
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	def GetValue : int()
	{
		return = x + y + z;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
pointList = Point_3D.ValueCtor(list1, 66, 88);
pointList_0_x = pointList[0].GetValue(); // 155
pointList_5_x = pointList[5].GetValue(); // 160
pointList_9_x = pointList[9].GetValue(); // 164";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("pointList_0_x", 155, 0);
            thisTest.Verify("pointList_5_x", 160, 0);
            thisTest.Verify("pointList_9_x", 164, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T22_Pass_1_single_list_of_class_type_and_1_variable_of_class_type()
        {
            string code = @"
class Integer
{
	value : int;
	
	constructor ValueCtor(_value : int)
	{
		value = _value;
	}
	
	def Mul : int(i1 : Integer, i2 : Integer)
	{
		return = i1.value * value * i2.value;
	}
}
list = {
			Integer.ValueCtor(4),
			Integer.ValueCtor(5),
			Integer.ValueCtor(7)
		};
i = Integer.ValueCtor(2);
m = i.Mul(list, i); // { 16, 20, 28 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _m = new List<Object> { 16, 20, 28 };
            Assert.IsTrue(mirror.CompareArrays("m", _m, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T23_Pass_2_lists_of_class_type_with_different_length()
        {
            string code = @"
class Integer
{
	value : int;
	
	constructor ValueCtor(_value : int)
	{
		value = _value;
	}
	
	def Mul : int(i1 : Integer, i2 : Integer)
	{
		return = i1.value * value * i2.value;
	}
}
list1 = {
			Integer.ValueCtor(4),
			Integer.ValueCtor(5),
			Integer.ValueCtor(7)
		};
list2 = { 
			Integer.ValueCtor(1),
			Integer.ValueCtor(2)
		};
		
i = Integer.ValueCtor(2);
m = i.Mul(list1, list2); // { 8, 20 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _m = new List<Object> { 8, 20 };
            Assert.IsTrue(mirror.CompareArrays("m", _m, typeof(System.Int64)));
        }
        [Ignore]
        public void T24_Pass_3x3_List_And_2x4_List()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Div : int(num1 : int, num2 : int)
	{
		return = (num1 + num2) / a;
	}
}
list1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
list2 = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
m = Math.ValueCtor(2);
list3 = m.Div(list1, list2);  // { { 1, 2, 3 }, { 4, 5, 6 } }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            // List<List<Object>> _list3 = new List<List<Object>>() { { 1, 2, 3 }, { 4, 5, 6 } }
            // Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Int64)));
        }

        [Test]
        [Category("Replication")]
        public void T25_Pass_3_List_Different_Length()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Div : int(num1 : int, num2 : int, num3 : int)
	{
		return = (num1 + num2 + num3) / a;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7 };
list2 = { 1, 2, 3, 4, 5 };
list3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
m = Math.ValueCtor( 2 ); 
list4 = m.Div(list1, list2, list3); // { 1.5,3.0,4.5,6.0,7.5} }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467229 - Sprint25 : REGRESSION : rev 3398 : replication over class method causes type conversion issue");
            thisTest.Verify("list4", new object[] { 2, 3, 5, 6, 8 });
        }

        [Test]
        [Category("Replication")]
        public void T26_Pass_3_List_Different_Length_2_Integers()
        {
            string src = @"class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Div : int(num1 : int, num2 : int, num3 : int, num4 : int, num5 : int)
	{
		return = (num1 + num2 + num3 + num4 + num5) / a;
	}
}
list1 = { 10, 11, 12, 13, 14, 15, 16 };
list2 = { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
list3 = { 30, 31, 32, 33, 34 };
m = Math.ValueCtor(4); 
listX2 = m.Div(list1, list2, list3, 15, 25); // { 25, 25, 26, 27, 28 }";
            thisTest.RunScriptSource(src);
            //Assert.Fail("1467229 - Sprint25 : REGRESSION : rev 3398 : replication over class method causes type conversion issue");
            thisTest.Verify("listX2", new object[] { 25, 26, 27, 27, 28 });
        }

        [Test]
        [Category("Replication")]
        public void T27_Pass_3_List_Same_Length()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num1 : int, num2 : int, num3 : int)
	{
		return = (num1 + num2 + num3) * a;
	}
}
list1 = { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
list2 = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
list3 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
m = Math.ValueCtor(4); 
list4 = m.Mul(list1, list2, list3); // { 252, 264, 276, 288, 300, 312, 324, 336, 348, 360 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list4 = new List<Object> { 252, 264, 276, 288, 300, 312, 324, 336, 348, 360 };
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Int64)));
        }

        [Test]
        [Category("Replication")]
        public void T28_Pass_3_List_Same_Length_2_Integers()
        {
            string src = @"class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Div : int(num1 : int, num2 : int, num3 : int, num4 : int, num5 : int)
	{
		return = (num1 + num2 + num3 + num4 + num5) / a;
	}
}
list1 = { 10, 11, 12, 13, 14 };
list2 = { 20, 21, 22, 23, 24 };
list3 = { 30, 31, 32, 33, 34 };
m = Math.ValueCtor(4); 
list2 = m.Div(list1, list2, list3, 15, 25);";
            thisTest.RunScriptSource(src);
            //Assert.Fail("1467229 - Sprint25 : REGRESSION : rev 3398 : replication over class method causes type conversion issue");
            thisTest.Verify("list2", new object[] { 25, 26, 27, 27, 28 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T29_Pass_FunctionCall_Reutrn_List001()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num : int)
	{
		return = num * a;
	}
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
m = Math.ValueCtor(10);
list2 = m.Mul(m.Mul(list1));  // { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T30_Pass_FunctionCall_Reutrn_List002()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num : int)
	{
		return = num * a;
	}
}
def foo : int (a : int)
{
	return = a * a;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
m = Math.ValueCtor(10);
list2 = m.Mul(foo(list1));  // { 10, 40, 90, 160, 250, 360, 490, 640, 810, 1000 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 10, 40, 90, 160, 250, 360, 490, 640, 810, 1000 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T31_Pass_FunctionCall_Reutrn_List003()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num : int)
	{
		return = num * a;
	}
}
def foo : int (a : int)
{
	return = a * a;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
m = Math.ValueCtor(10);
list2 = foo(m.Mul(list1));  // { 100, 400, 900, 1600, 2500, 3600, 4900, 6400, 8100, 10000 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 100, 400, 900, 1600, 2500, 3600, 4900, 6400, 8100, 10000 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T32_Pass_Single_3x3_List()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num : int)
	{
		return = num * a;
	}
}
list1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
m = Math.ValueCtor(10);
list2 = m.Mul(list1);  // { { 10, 20, 30 }, { 40, 50, 60 }, { 70, 80, 90 } }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            // List<List<Object>> _list2 = new List<List<Object>>() { { 10, 20, 30 }, { 40, 50, 60 }, { 70, 80, 90 } };
            // Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T33_Pass_Single_List()
        {
            string code = @"
class Math
{
	a : double;
	
	constructor ValueCtor(_a : double)
	{	
		a = _a;
	}
	
	def Mul : double(num : double)
	{
		return = num * a;
	}
}
list1 = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
m = Math.ValueCtor(10.0);
list2 = m.Mul(list1);  // { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T34_Pass_Single_List_2_Integers()
        {
            string code = @"
class Math
{
	a : int;
	
	constructor ValueCtor(_a : int)
	{	
		a = _a;
	}
	
	def Mul : int(num1 : int, num2 : int, num3 : int)
	{
		return = (num1 + num2 + num3) * a;
	}
}
list1 = { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
m = Math.ValueCtor(5);
list2 = m.Mul(list1, 12, 17); // {300,305,310,315,320,325,330,335,340,345}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 300, 305, 310, 315, 320, 325, 330, 335, 340, 345 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T35_Pass_1_list_of_class_type_and_1_variable_of_class_type()
        {
            //Assert.Fail("1467194 - Sprint 25 - rev Regressions created by array copy constructions ");
            string code = @"
class Point_3D
{
	x : int;
	y : int;
	z : int;
	
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	
	def GetCoor(type : int)
	{
		return = type == 1 ? x : type == 2 ? y : z;
	}
}
def GetMidPoint : Point_3D(p1 : Point_3D, p2 : Point_3D)
{
	return = Point_3D.ValueCtor(	
									(p1.GetCoor(1) + p2.GetCoor(1)), 
									(p1.GetCoor(2) + p2.GetCoor(2)), 
									(p1.GetCoor(3) + p2.GetCoor(3)) 
								);
}
list1 = { 
			Point_3D.ValueCtor(1, 2, 3),
			Point_3D.ValueCtor(4, 5, 6),
			Point_3D.ValueCtor(7, 8, 9)
		};
var2 = Point_3D.ValueCtor(10, 10, 10);
list3 = GetMidPoint(list1, var2);
list3_0_x = list3[0].GetCoor(1); // 11
list3_1_y = list3[1].GetCoor(2); // 15
list3_2_z = list3[2].GetCoor(3); // 19
			";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list3_0_x", 11, 0);
            thisTest.Verify("list3_1_y", 15, 0);
            thisTest.Verify("list3_2_z", 19, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T36_Pass_1_single_list_of_class_type()
        {
            string code = @"
class Integer
{
	value : int;
	
	constructor ValueCtor(_value : int)
	{
		value = _value;
	}
}
def Square : int(i : Integer)
{
	return = i.value * i.value;
}
list = 	{
			Integer.ValueCtor(2),
			Integer.ValueCtor(3),
			Integer.ValueCtor(4),
			Integer.ValueCtor(5)
		};
		
list2 = Square(list); // { 4, 9, 16, 25 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 4, 9, 16, 25 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T37_Pass_2_lists_of_class_type_different_length()
        {
            string code = @"
class Point_3D
{
	x : int;
	y : int;
	z : int;
	
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	
	def GetCoor(type : int)
	{
		return = type == 1 ? x : type == 2 ? y : z;
	}
}
def GetMidPoint : Point_3D(p1 : Point_3D, p2 : Point_3D)
{
	return = Point_3D.ValueCtor(	
									(p1.GetCoor(1) + p2.GetCoor(1)), 
									(p1.GetCoor(2) + p2.GetCoor(2)), 
									(p1.GetCoor(3) + p2.GetCoor(3)) 
								);
}
list1 = { 
			Point_3D.ValueCtor(1, 2, 3),
			Point_3D.ValueCtor(4, 5, 6),
			Point_3D.ValueCtor(7, 8, 9)
		};
list2 = {
			Point_3D.ValueCtor(10, 10, 10),
			Point_3D.ValueCtor(20, 20, 20)
		};
list3 = GetMidPoint(list1, list2);
list3_0_x = list3[0].GetCoor(1); // 11
list3_1_y = list3[1].GetCoor(2); // 25
			";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list3_0_x", 11, 0);
            thisTest.Verify("list3_1_y", 25, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T38_Pass_2_lists_of_class_type_different_length_and_1_integer()
        {
            string code = @"
class Integer
{
	value : int;
	constructor ValueCtor(_value : int)
	{
		value =  _value;
	}
}
def Sum : int(i1 : Integer, i2 : Integer, i3 : int)
{
	return = i1.value + i2.value + i3;
}
list1 = {
			Integer.ValueCtor(2),
			Integer.ValueCtor(5),
			Integer.ValueCtor(8)
		};
list2 = {
			Integer.ValueCtor(3),
			Integer.ValueCtor(6),
			Integer.ValueCtor(9),
			Integer.ValueCtor(12)
		};
list3 = Sum(list1, list2, 10); // { 15, 21, 27 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { 15, 21, 27 };
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T39_Pass_2_lists_of_class_type_same_length_and_1_variable_of_class_type()
        {
            string code = @"
class Integer
{
	value : int;
	constructor ValueCtor(_value : int)
	{
		value =  _value;
	}
}
def Sum : int(i1 : Integer, i2 : Integer, i3 : Integer)
{
	return = i1.value + i2.value + i3.value;
}
list1 = {
			Integer.ValueCtor(2),
			Integer.ValueCtor(5),
			Integer.ValueCtor(8)
		};
list2 = {
			Integer.ValueCtor(3),
			Integer.ValueCtor(6),
			Integer.ValueCtor(9)
		};
list3 = Sum(list1, list2, Integer.ValueCtor(10)); // { 15, 21, 27 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { 15, 21, 27 };
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T40_Pass_2_List_of_class_type_Same_Length()
        {
            string code = @"
class Point_3D
{
	x : var;
	y : var;
	z : var;
	
	constructor ValueCtor(_x : int, _y : int, _z : int)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	
	def GetCoor(type : int)
	{
		return = type == 1 ? x : type == 2 ? y : z;
	}
}
def GetMidPoint : Point_3D(p1 : Point_3D, p2 : Point_3D)
{
	return = Point_3D.ValueCtor(	
									(p1.GetCoor(1) + p2.GetCoor(1)), 
									(p1.GetCoor(2) + p2.GetCoor(2)), 
									(p1.GetCoor(3) + p2.GetCoor(3)) 
								);
}
list1 = { 
			Point_3D.ValueCtor(1, 2, 3),
			Point_3D.ValueCtor(4, 5, 6),
			Point_3D.ValueCtor(7, 8, 9)
		};
list2 = { 
			Point_3D.ValueCtor(10, 11, 12),
			Point_3D.ValueCtor(13, 14, 15),
			Point_3D.ValueCtor(16, 17, 18)
		};
list3 = GetMidPoint(list1, list2);
list3_0_x = list3[0].GetCoor(1); // 11
list3_1_y = list3[1].GetCoor(2); // 19
list3_2_z = list3[2].GetCoor(3); // 27
			";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list3_0_x", 11, 0);
            thisTest.Verify("list3_1_y", 19, 0);
            thisTest.Verify("list3_2_z", 27, 0);
        }

        [Test]
        [Category("Replication")]
        public void T41_Pass_3x3_List_And_2x4_List()
        {
            string code = @"
def foo : int(a : int, b : int)
{
	return = a * b;
}
list1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
list2 = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
list3 = foo(list1, list2); // { { 1, 4, 9 }, { 20, 30, 42 } }
x = list3[0];
y = list3[1];";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            List<Object> _list1 = new List<Object>() { 1, 4, 9 };
            List<Object> _list2 = new List<Object>() { 20, 30, 42 };
            Assert.IsTrue(mirror.CompareArrays("x", _list1, typeof(System.Int64)));
            Assert.IsTrue(mirror.CompareArrays("y", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T42_Pass_3_List_Different_Length()
        {
            string code = @"
def foo : int(a : int, b : int, c : int)
{
	return = a * b - c;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3 };
list3 = {1, 4, 7, 2, 5, 8, 3 };
list4 = foo(list1, list2, list3); // { 9, 14, 17, 26, 25, 22, 25 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list4 = new List<Object> { 9, 14, 17, 26, 25, 22, 25 };
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Int64)));
        }

        [Test]
        [Category("Replication")]
        public void T43_Pass_3_List_Different_Length_2_Integers()
        {
            string src = @"def foo : int(a : int, b : int, c : int, d : int, e : int)
{
	return = a * b - c / d + e;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3 };
list3 = {1, 4, 7, 2, 5, 8, 3 };
list4 = foo(list1, list2, list3, 4, 23);";
            thisTest.RunScriptSource(src);
            //Assert.Fail("1467229 - Sprint25 : REGRESSION : rev 3398 : replication over class method causes type conversion issue");

            thisTest.Verify("list4", new object[] { 33, 40, 45, 51, 52, 51, 50 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T44_Pass_3_List_Same_Length()
        {
            string code = @"
def foo : int(a : int, b : int, c : int)
{
	return = a * b - c;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
list3 = {1, 4, 7, 2, 5, 8, 3, 6, 9, 0 };
list4 = foo(list1, list2, list3); // { 9, 14, 17, 26, 25, 22, 25, 18, 9, 10 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list4 = new List<Object> { 9, 14, 17, 26, 25, 22, 25, 18, 9, 10 };
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T45_Pass_3_List_Same_Length_2_Integers()
        {
            string code = @"
def foo : int(a : int, b : int, c : int, d : int, e : int)
{
	return = a * b - c * d + e;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
list3 = {1, 4, 7, 2, 5, 8, 3, 6, 9, 0 };
list4 = foo(list1, list2, list3, 26, 43); // { 27, -43, -115, 19, -57, -135, -7, -89, -173, 53 }  ";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list4 = new List<Object> { 27, -43, -115, 19, -57, -135, -7, -89, -173, 53 };
            Assert.IsTrue(mirror.CompareArrays("list4", _list4, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T46_Pass_FunctionCall_Reutrn_List()
        {
            string code = @"
def foo : int(a : int)
{
	return = a * a;
}
list1 = { 1, 2, 3, 4, 5 };
list3 = foo(foo(foo(list1))); // { 1, 256, 6561, 65536, 390625 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list3 = new List<Object> { 1, 256, 6561, 65536, 390625 };
            Assert.IsTrue(mirror.CompareArrays("list3", _list3, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T47_Pass_Single_3x3_List()
        {
            string code = @"
def foo : int(a : int)
{
	return = a * a;
}
list1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
list2 = foo(list1); // { { 1, 4, 9 }, { 16, 25, 36 }, { 49, 64, 81 } }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            // List<List<Object>> _list2 = new List<List<Object>>() { { 1, 4, 9 }, { 16, 25, 36 }, { 49, 64, 81 } }
            //Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T48_Pass_Single_List()
        {
            string code = @"
def foo : int(num : int)
{
	return = num * num;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = foo(list1);  // { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("SmokeTest")]
        public void T49_Pass_Single_List_2_Integers()
        {
            string code = @"
def foo : int(num : int, num2 : int, num3 : int)
{
	return = num * num2 - num3;
}
list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
list2 = foo(list1, 34, 18); // { 16, 50, 84, 118, 152, 186, 220, 254, 288, 322 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { 16, 50, 84, 118, 152, 186, 220, 254, 288, 322 };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Int64)));
        }

        [Test]
        [Category("Replication")]
        public void T50_1_of_3_Exprs_is_List()
        {
            string code = @"
list1 = { true, false, true, false, true };
list2 = list1 ? 1 : 0; // { 1, 0, 1, 0, 1 }
list3 = true ? 10 : list2; // { 10, 10, 10, 10, 10 }
list4 = false ? 10 : list2; // { 1, 0, 1, 0, 1 }
a = { 1, 2, 3, 4, 5 };
b = {5, 4, 3, 2, 1 };
c = { 4, 3, 2, 1 };
list5 = a > b ? 1 : 0; // { 0, 0, 0, 1, 1 }
list6 = c > a ? 1 : 0; // { 1, 1, 0, 0 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1456751 : Sprint16 : Rev 990 : Inline conditions not working with replication over collections");
            thisTest.Verify("list2", new object[] { 1, 0, 1, 0, 1 });
            thisTest.Verify("list3", 10);
            thisTest.Verify("list4", new object[] { 1, 0, 1, 0, 1 });
            thisTest.Verify("list5", new object[] { 0, 0, 0, 1, 1 });
            thisTest.Verify("list6", new object[] { 1, 1, 0, 0 });
        }

        [Test]
        [Category("Replication")]
        public void T51_2_of_3_Exprs_are_Lists_Different_Length()
        {
            Object[] a1 = new Object[] { 1, 2, 3, 4, 5 };
            Object[] a2 = new Object[] { 2, 6, 10 };
            string code = @"
list1 = { 1, 2, 3, 4, 5 };
list2 = { true, false, true, false };
list3 = list2 ? list1 : 0; // { 1, 0, 3, 0 }
list4 = list2 ? 0 : list1; // { 0, 2, 0, 4 }
list5 = { -1, -2, -3, -4, -5, -6 };
list6 = true ? list1 : list5; // { 1, 2, 3, 4, 5 }
list7 = false ? list1 : list5; // { -1, -2, -3, -4, -5 }  
a = { 1, 2, 3, 4 };
b = { 5, 4, 3, 2, 1 };
c = { 1, 4, 7 };
list8 = a >= b ? a + c : 10; // { 10, 10, 10 }
list9 = a < b ? 10 : a + c; // { 10, 10, 10 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("list3", new object[] { a1, 0, a1, 0 });
            thisTest.Verify("list4", new object[] { 0, a1, 0, a1 });
            thisTest.Verify("list6", a1);
            thisTest.Verify("list7", new object[] { -1, -2, -3, -4, -5, -6 });
            thisTest.Verify("list8", new object[] { 10, 10, a2, a2 });
            thisTest.Verify("list9", new object[] { 10, 10, a2, a2 });
        }

        [Test]
        [Category("Replication")]
        public void T53_3_of_3_Exprs_are_different_dimension_list()
        {
            string code = @"
a = { { 1, 2, 3 }, { 4, 5, 6 } };
b = { { 1, 2 },  { 3, 4 }, { 5, 6 } };
c = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };
list = a > b ? b + c : a + c; // { { 2, 4, }, { 8, 10 } } ";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1456751 : Sprint16 : Rev 990 : Inline conditions not working with replication over collections");
            Object[] b1 = new Object[] { new Object[] { new Object[] { new Object[] { 2, 4, 6 }, new Object[] { 9, 11, 13 } }, new Object[] { new Object[] { 2, 4, 6 }, new Object[] { 9, 11, 13 } } }, new Object[] { new Object[] { new Object[] { 2, 4 }, new Object[] { 8, 10 }, new Object[] { 14, 16 } }, new Object[] { new Object[] { 2, 4 }, new Object[] { 8, 10 }, new Object[] { 14, 16 } } } };
            thisTest.Verify("list", b1);
        }

        [Test]
        [Category("Replication")]
        public void T54_3_of_3_Exprs_are_Lists_Different_Length()
        {
            Object[] list2 = { 1, 2, 3, 4 };
            Object[] list3 = { -1, -2, -3, -4, -5, -6 };
            Object[] list4 = new Object[] { list2, list3, list2, list2, list3 };
            Object[] list5 = new Object[] { list4, list2, list4, list4, list2 };
            Object[] list6 = { -1, -2, -3, -4, -5 };
            Object[] list7 = new Object[] { list2, list6, list2, list2, list6 };
            Object[] list8 = new Object[] { new Object[] { 1, 4, 0 }, new Object[] { 0, 5, 1, 5 }, new Object[] { 0, 5, 1, 5 } };
            string code = @"
list1 = { true, false, true, true, false };
list2 = { 1, 2, 3, 4 };
list3 = { -1, -2, -3, -4, -5, -6 };
list4 = list1 ? list2 : list3; // { 1, -2, 3, 4 }
list5 = !list1 ? list2 : list4; // { 1, 2, 3, 4 }
list6 = { -1, -2, -3, -4, -5 };
list7 = list1 ? list2 : list6; // { 1, -2, 3, 4 }
a = { 3, 0, -1 };
b = { 2, 1, 0, 3 };
c = { -2, 4, 1, 2, 0 };
list8 = a < c ? b + c : a + c; // { 1, 4, 1 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1456751 : Sprint16 : Rev 990 : Inline conditions not working with replication over collections");
            thisTest.Verify("list4", list4);
            thisTest.Verify("list5", list5);
            thisTest.Verify("list7", list7);
            thisTest.Verify("list8", list8);
        }

        [Test]
        [Category("Replication")]
        public void T55_3_of_3_Exprs_are_Lists_Same_Length()
        {
            Object[] list2 = { 1, 2, 3, 4 };
            Object[] list3 = { -1, -2, -3, -4 };
            Object[] list6 = new Object[] { new Object[] { -4, -1, 2 }, new Object[] { -4, -1, 2 }, new Object[] { 8, 17, 8 } };
            Object[] list5 = new Object[] { list3, list2, list2, list3 };
            Object[] list4 = new Object[] { list2, list3, list3, list2 };
            string code = @"
list1 = { true, false, false, true };
list2 = { 1, 2, 3, 4 };
list3 = { -1, -2, -3, -4 };
list4 = list1 ? list2 : list3; // { 1, -2, -3, 4 }
list5 = !list1 ? list2 : list3; // { -1, 2, 3, -4 }
a = { 1, 4, 7 };
b = { 2, 8, 5 };
c = { 6, 9, 3 };
list6 = a > b ? b + c : b - c; // { -4, -1, 8 }";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1456751 : Sprint16 : Rev 990 : Inline conditions not working with replication over collections");
            thisTest.Verify("list4", list4);
            thisTest.Verify("list5", list5);
            thisTest.Verify("list6", list6);
        }

        [Test]
        [Category("SmokeTest")]
        public void T56_UnaryOperator()
        {
            string code = @"
list1 = { true, true, false, false, true, false };
list2 = !list1; // { false, false, true, true, false, true }
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> _list2 = new List<Object> { false, false, true, true, false, true };
            Assert.IsTrue(mirror.CompareArrays("list2", _list2, typeof(System.Boolean)));
        }

        [Test]
        [Category("Replication")]
        public void T50_Replication_Imperative_Scope()
        {
            string code = @"
c;
[Imperative]
{
	def even : int (a : int) 
	{	
		if(( a % 2 ) > 0 )
			return = a + 1;		
		else 
			return = a;
		
		return = 0;
	}
    x = { 1, 2, 3 };
	c = even(x);
	
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2, 2, 4 };
            thisTest.Verify("c", v1);
            ;
        }

        [Test]
        [Category("SmokeTest")]
        public void SimpleOp()
        {
            String code =
@"x;test;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            ProtoCore.Lang.Obj o = mirror.GetValue("x");
            Assert.IsTrue((Int64)o.Payload == 5);
            ProtoCore.Lang.Obj o2 = mirror.GetValue("test");
            Assert.IsTrue((Int64)o2.Payload == 10);
        }

        [Test]
        [Category("SmokeTest")]
        public void ArraySimpleOp()
        {
            String code =
@"foo;test;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            ProtoCore.Lang.Obj o = mirror.GetValue("foo");
            ProtoCore.DSASM.Mirror.DsasmArray a = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue(a.members.Length == 1);
            Assert.IsTrue((Int64)a.members[0].Payload == 5);
            ProtoCore.Lang.Obj o2 = mirror.GetValue("test");
            ProtoCore.DSASM.Mirror.DsasmArray a2 = (ProtoCore.DSASM.Mirror.DsasmArray)o2.Payload;
            Assert.IsTrue(a2.members.Length == 1);
            Assert.IsTrue((Int64)a2.members[0].Payload == 10);
        }

        [Test]
        [Category("SmokeTest")]
        public void ArraySimpleCall01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            ProtoCore.Lang.Obj o = mirror.GetValue("test");
            ProtoCore.DSASM.Mirror.DsasmArray a = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue(a.members.Length == 1);
            Assert.IsTrue((Int64)a.members[0].Payload == 10);
        }

        [Test]
        [Category("SmokeTest")]
        public void ArraySimpleCall02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x").Payload == 14);
        }

        [Test]
        [Category("SmokeTest")]
        public void ArraySimpleCall03()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x1").Payload == 12);
            Assert.IsTrue((double)mirror.GetValue("x2").Payload == 13);
            Assert.IsTrue((double)mirror.GetValue("x3").Payload == 14);
        }

        [Test]
        [Category("SmokeTest")]
        public void ArraySimpleCall04()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 32);
        }

        [Test]
        [Category("SmokeTest")]
        public void TestSimpleCallAssociative()
        {
            String code =
@"def fun : double() { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            Assert.IsTrue((Double)o.Payload == 4);
        }

        [Test]
        [Category("SmokeTest")]
        public void TestSimpleCallImperative()
        {
            String code =
@"def fun : double() { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            Assert.IsTrue((Double)o.Payload == 4);
        }

        [Test]
        [Category("SmokeTest")]
        public void TestSimpleCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            Assert.IsTrue((Double)o.Payload == 4);
        }

        [Test]
        [Category("SmokeTest")]
        public void Test1D1CellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new Object[] { 4.0 });
        }

        [Test]
        [Category("Replication")]
        public void Test1DDeepNestCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { 4.0 } } } } } } } } });
        }

        [Test]
        [Category("Replication")]
        public void Test1D2DeepNestCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            Object[] v1 = new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { new Object[] { 4.0, 4.0 } } } } } } } } };
            TestFrameWork.Verify(mirror, "a", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void Test1DnCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { 4.0, 4.0, 4.0, 4.0 });
        }

        [Test]
        [Category("Replication")]
        public void Test2DnSquareCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { 
        }

        [Test]
        [Category("Replication")]
        public void Test2DnJaggedCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { 
        }

        [Test]
        [Category("SmokeTest")]
        public void Test1D1DSimpleCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double, arg2:double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            Assert.IsTrue(!o.Type.IsIndexable);
            Assert.IsTrue((Double)o.Payload == 4);
        }

        [Test]
        [Category("SmokeTest")]
        public void Test1D1D2CallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double, arg2:double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { 4.0, 4.0, 4.0, 4.0 });
        }

        [Test]
        [Category("SmokeTest")]
        public void Test1D1DCellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double, arg2:double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { 4.0 });
        }

        [Test]
        [Category("Replication")]
        public void Test2D2CellArrayCallWithArgAssociative()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "a", new Object[] { new Object[] { 4.0 }, new Object[] { 4.0 } });
        }

        [Test]
        [Category("SmokeTest")]
        public void TestIncompatibleTypes()
        {
            String code =
@"def fun : double(arg: int) { return = 4; }
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "v2", 4.0);
        }

        [Test]
        [Category("SmokeTest")]
        public void TestOverloadDispatchWithTypeConversion()
        {
            String code =
@"class TestDefect
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            TestFrameWork.Verify(mirror, "s", -123);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Defect_1456568_Replication_On_Operators()
        {
            String code =
@"xdata = {1, 2};
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork fx = new TestFrameWork();
            Object[] v1 = new Object[] { 4, 6 };
            TestFrameWork.Verify(mirror, "z", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Defect_1456568_Replication_On_Operators_2()
        {
            String code =
@"xdata = {1, 2};
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 3, 8 };
            thisTest.Verify("z", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Defect_1456568_Replication_On_Operators_3()
        {
            String code =
@"xdata = {1, 2, 5, 7};
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { -2, -2 };
            thisTest.Verify("z", v1);
        }

        [Test]
        [Category("Replication")]
        public void T09_Defect_1456568_Replication_On_Operators_4()
        {
            String code =
@"
            //Assert.Fail("1467089 - Sprint23 : rev 2681 : replication issue ComplierInternalException when using replication over array of instances");
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { null, 1, null, null };
            thisTest.Verify("z", v1);
        }

        [Test]
        [Category("Replication")]
        public void T09_Defect_1456568_Replication_On_Operators_5()
        {
            String code =
@"
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 4.5, 6.0 };
            Object[] v2 = new Object[] { 6.0, 8.0 };
            thisTest.Verify("x", v1);
            thisTest.Verify("y", v2);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Defect_1456568_Replication_On_Operators_6()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v2 = new Object[] { -2, -2 };
            thisTest.Verify("z1", v2);
            thisTest.Verify("z2", v2);
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_Defect_1456568_Replication_On_Operators_7()
        {
            String code =
@"
            string errmsg = "";//DNL-1467450 Rev 4596 : Regression : Need consistence in error generated for undefined class instances";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { 0.5, 0.5 };
            thisTest.Verify("z1", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T57_Defect_1467004_Replication_With_Method_Overload()
        {
            String code =
                            @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("s", -123);
        }

        [Test]
        public void T57_Defect_1467004_Replication_With_Method_Overload_2()
        {
            String code =
                            @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            // Assert.Fail("1467091 - Sprint24 : rev 2733 : instance method resolution is too tolerant and passing even when the instance is not defined");
            Object n1 = null;
            thisTest.Verify("s", n1);
        }

        [Test]
        [Category("Replication")]
        public void T57_Defect_1467004_Replication_With_Method_Overload_3()
        {
            String code =
                            @"
            string errmsg = "1467090 - Sprint24 : rev 2733 : Replication and Method resolution issue : type conversion should be of lower precedence than exact match";

            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { 3, 3, 4, 3 };
            thisTest.Verify("s", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T57_Defect_1467004_Replication_With_Method_Overload_4()
        {
            String code =
                            @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2, 2, 2 };
            thisTest.Verify("s", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T57_Defect_1467004_Replication_With_Method_Overload_5()
        {
            String code =
                            @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("s", 1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T57_Defect_1467004_Replication_With_Method_Overload_6()
        {
            String code =
                            @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("s", 1);
        }

        [Test]
        [Category("Replication")]
        public void T57_Defect_1467004_Replication_With_Method_Overload_7()
        {
            String code =
                            @"  def foo(val : int[])
            string errmsg = "1467090 - Sprint24 : rev 2733 : Replication and Method resolution issue : type conversion should be of lower precedence than exact match";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v2 = new Object[] { 3, 4 };
            Object[] v1 = new Object[] { 1, 3, 4, 2, v2, 3 };
            thisTest.Verify("s", v1);// to be verified when defect is fixed
        }

        [Test]
        [Category("SmokeTest")]
        public void T58_Defect_1456115_Replication_Over_Collections()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2, 4, 6 };
            thisTest.Verify("y", v1);
        }

        [Test]
        [Category("Replication")]
        public void T58_Defect_1456115_Replication_Over_Collections_2()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            thisTest.Verify("t1", 5.0);
            thisTest.Verify("t2", 3.0);
        }

        [Test]
        [Category("SmokeTest")]
        public void T58_Defect_1456115_Replication_Over_Collections_3()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2 };
            thisTest.Verify("y", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T58_Defect_1456115_Replication_Over_Collections_4()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2 };
            thisTest.Verify("y", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T58_Defect_1456115_Replication_Over_Collections_5()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 2, 4, null };
            thisTest.Verify("y", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T58_Defect_1456115_Replication_Over_Collections_6()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { 1, 3, null };
            thisTest.Verify("y", v1);
        }

        [Test]
        [Category("Update")]
        public void T58_Defect_1456115_Replication_Over_Collections_7()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { null, 5 };
            thisTest.Verify("y", v1);
            thisTest.Verify("test", 5);
        }

        [Test]
        [Category("Update")]
        public void T58_Defect_1456115_Replication_Over_Collections_8()
        {
            String code =
@"
            string errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { null, 5 };
            thisTest.Verify("y", v1);
            thisTest.Verify("test", 5);
        }

        [Test]
        [Category("SmokeTest")]
        public void T59_Defect_1463351_Replication_Over_Unary_Operators()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { false, true };
            thisTest.Verify("list2", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T59_Defect_1463351_Replication_Over_Unary_Operators_2()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { false, null, null, true, true, false, false, false };
            thisTest.Verify("list2", v1);
        }

        [Test]
        [Category("Replication")]
        public void T59_Defect_1463351_Replication_Over_Unary_Operators_3()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { new Object[] { false, null }, true, false };
            //Assert.Fail("1467183 - Sprint24: rev 3163 : replication on nested array is outputting extra brackets in some cases");
            thisTest.Verify("list2", v1);
        }

        [Test]
        [Category("Replication")]
        public void T59_Defect_1463351_Replication_Over_Unary_Operators_4()
        {
            String code =
@"
            //Assert.Fail("1467096 - Sprint24: rev 2759 : Replication using unary operators over class instances causes CompilerInternalAssertion");
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v1 = new Object[] { false, false };
            thisTest.Verify("b", v1);
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 10);
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c1", 10);
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c1", 2);
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object n1 = null;
            thisTest.Verify("c1", 1); // this depends on new replication logic : please check
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c1", 2); // this depends on new replication logic : please check
        }

        [Test]
        [Category("SmokeTest")]
        public void T60_Defect_1455247_Replication_Over_Class_Instances_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c1", 3); // this depends on new replication logic : please check
        }

        [Test]
        [Category("Replication")]
        public void T61_Defect_1463338_Replication_CallSite_Assertion()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            thisTest.Verify("list2_0_0", 1);
        }

        [Test]
        public void T50_Defect_1456738_Replication_Race_Condition()
        {
            string code = @"
//import ( ""Math.dll"");
class Math
{
   static def Sin ( x1 : double)
   {
       return = x1;
   }
}
// dimensions of the roof in each direction
//
xSize = 10;
ySize = 20;
// number of Waves in each direction
//
xWaves = 1;
yWaves = 3;
// number of points per Wave in each direction\
//
xPointsPerWave = 10;
yPointsPerWave = 10;
// amplitudes of the frequencies (z dimension)
//
lowFrequencyAmpitude = 1.0; // only ever a single low frequency wave
highFrequencyAmpitude = 0.75; // user controls the number and amplitude of high frequency waves 
// dimensions of the beams
//
radius = 0.1;
roofWallHeight = 0.3; // not used
roofWallThickness = 0.1; // not used
// calculate how many 180 degree cycles we need for the Waves
//
x180ToUse = xWaves==1?xWaves:(xWaves*2)-1;
y180ToUse = yWaves==1?yWaves:(yWaves*2)-1;
// count of total number of points in each direction
//
xCount = xPointsPerWave*xWaves;
yCount = yPointsPerWave*yWaves;
xHighFrequency = Math.Sin(0..(180*x180ToUse)..#xCount)*highFrequencyAmpitude;
xLowFrequency = Math.Sin(-5..185..#xCount)*lowFrequencyAmpitude;
yHighFrequency = Math.Sin(0..(180*y180ToUse)..#yCount)*highFrequencyAmpitude;
yLowFrequency = Math.Sin(-5..185..#yCount)*lowFrequencyAmpitude;
sinRange = {0.0, 10, 20, 30, 40 ,50, 60 ,70 ,80 , 90 ,100, 110 ,120, 130, 140, 150, 160, 170};
xHighFrequency = Math.Sin(sinRange) * highFrequencyAmpitude;
y = Count(xHighFrequency);";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 18);
        }

        [Test]
        [Category("Replication")]
        public void T62_Defect_1467075_replication_on_nested_array()
        {
            String code =
@"def fun : double(arg: double) { return = 4.0; }
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467075 - Sprint23 : rev 2660 : replication with nested array is not working as expected");
            thisTest.Verify("a", new Object[] { new Object[] { 4.0 }, new Object[] { 4.0 } });
        }
        [Test, Ignore]
        [Category("Replication")]
        public void T63_Defect_1467177_replication_in_imperative()
        {
            // need to move this to post R1 project

            String code =
@"[Imperative]
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            string err = "1467177 - sprint24: rev 3152 : REGRESSION : Replication should not be supported in Imperative scope";
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);

            thisTest.Verify("d", null);
        }

        [Test]
        [Category("Replication")]
        public void T64_Defect_1456105_replication_on_function_with_no_arg_type()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("d", new Object[] { 2, 3, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T64_Defect_1456105_replication_on_function_with_no_arg_type_2()
        {
            String code =
@"def foo2 : double (arr : double)
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("sum1", new Object[] { 0.0, 0.0, 0.0, 0.0 });
        }

        [Test]
        [Category("Replication")]
        public void T64_Defect_1456105_replication_on_function_with_no_arg_type_3()
        {
            String code =
@"def foo2  (x )
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("sum1", new Object[] { 2, 3.0, 4, 5 });
            thisTest.Verify("sum2", new Object[] { 2.0, 3.0, 4.0, 5.0 });
        }

        [Test]
        [Category("Replication")]
        [Category("SmokeTest")]
        public void T65_Defect_1467125_Geo_Replication()
        {
            string code = @"
import(""ProtoGeometry.dll"");
WCS = CoordinateSystem.Identity();
// create initialPoints
diafac1 = 1;
h1 = 15;
hL1=10;
pt0 = Point.ByCartesianCoordinates(WCS,-5*diafac1,-5*diafac1,0);
pt1 = Point.ByCartesianCoordinates(WCS,5*diafac1,-5*diafac1,0);
pt2 = Point.ByCartesianCoordinates(WCS,5*diafac1,5*diafac1,0);
pt3 = Point.ByCartesianCoordinates(WCS,-5*diafac1,5*diafac1,0);
pt4 = Point.ByCartesianCoordinates(WCS,-5*diafac1,-5*diafac1,hL1);
pt5 = Point.ByCartesianCoordinates(WCS,5*diafac1,-5*diafac1,hL1);
pt6 = Point.ByCartesianCoordinates(WCS,5*diafac1,5*diafac1,hL1);
pt7 = Point.ByCartesianCoordinates(WCS,-5*diafac1,5*diafac1,hL1);
pt8 = Point.ByCartesianCoordinates(WCS,-15,-15,h1);
pt9 = Point.ByCartesianCoordinates(WCS,15,-15,h1);
pt10= Point.ByCartesianCoordinates(WCS,15,15,h1);
pt11 = Point.ByCartesianCoordinates(WCS,-15,15,h1);
pointGroup = {pt0,pt1,pt2,pt3,pt4,pt5,pt6,pt7,pt8,pt9,pt10,pt11};
facesIndices = {{0,1,5,4},{1,2,6,5},{2,3,7,6},{3,0,4,7},{4,5,9,8},{5,6,10,9},{6,7,11,10},{7,4,8,11}};
groupOfPointGroups =  { { pt0, pt1, pt2 }, { pt3, pt4, pt5 }, { pt6, pt7, pt8 }, { pt9, pt10, pt11 } };
simplePointGroup = {pt5, pt6, pt10, pt9};
// note: Polygon.ByVertices expects a 1D array of points.. so let`s test this 
controlPolyA = Polygon.ByVertices({pt0, pt1, pt5, pt4}); // OK with 1D collection
controlPolyB = Polygon.ByVertices(simplePointGroup); // OK with 1D collection
	controlPolyC = Polygon.ByVertices({{pt1, pt2, pt6, pt5},{pt2, pt3, pt7, pt6}}); // not OK with literal 2D collection
														// get compiler error `unable to locate mamaged object for given dsObject`
	controlPolyD = Polygon.ByVertices(pointGroup[3]);    // not OK with a 1D subcollection a a member indexed from a 2D collection
														// controlPolyD = null
	controlPolyE = Polygon.ByVertices(pointGroup[facesIndices]); // not OK with an array of indices
																// controlPolyE = null
controlPolyF = Polygon.ByVertices(groupOfPointGroups);
// result = foo({ controlPolyA, controlPolyB, controlPolyC, controlPolyD, controlPolyE });
/*def foo(x:Polygon)
{
	if (x!= null)
	{
	    return = true;
	}
	else return = false;
}*/
//a simple case
c=2 * {{1},{2}};";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Object[] v1 = new Object[] {true,true,true,false,false};
            Object v1 = null;
            Assert.IsTrue(mirror.GetValue("controlPolyA").Payload != v1);
            Assert.IsTrue(mirror.GetValue("controlPolyB").Payload != v1);
            Assert.IsTrue(mirror.GetValue("controlPolyC").Payload != v1);
            Assert.IsTrue(mirror.GetValue("controlPolyD").Payload == v1);
            Assert.IsTrue(mirror.GetValue("controlPolyE").Payload != v1);
            //thisTest.Verify("controlPolyA",v2);
        }

        [Test]
        [Category("Replication")]
        public void T66_Defect_1467125_Replication_Method()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            string err = "1467125 - Sprint 24 - Rev 2877 replication does not work with higher ranks , throws error Method resolution failure";
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);

            Object[] v1 = new Object[] { 10, 11 };
            Object[] v2 = new Object[] { 20, 22 };
            Object[] v3 = new Object[] { v1, v2 };
            Object[] v4 = new Object[] { v1, v2 };
            thisTest.Verify("rab", v3);
        }

        [Test]
        [Category("Replication")]
        public void T66_Defect_1467125_Replication_Method_2()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            string err = "1467125 - VALIDATION NEEDED - Sprint 24 - Rev 2877 replication does not work with higher ranks , throws error Method resolution failure";
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);

            Object[] v1 = new Object[] { 10, 11 };
            Object[] v2 = new Object[] { 20, 22 };
            Object[] v3 = new Object[] { v1, v2 };
            Object[] v4 = new object[] { new object[] { new object[] { 1 }, new object[] { 2 } }, new object[] { new object[] { 2 }, new object[] { 4 } } };
            Object[] v5 = new Object[] { new object[] { new object[] { 0 } }, new object[] { new object[] { 0 } } };
            thisTest.Verify("rab", v3);
            thisTest.Verify("rac", v4);
            thisTest.Verify("rad", v5);
        }

        [Test]
        public void Test()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void Array_Ranks_Match_argumentdefinition_1467190()
        {
            String code =
            @"
            //Assert.Fail("1467190 -Sprint 25 - Rev 3185 it replicates when not expected ");
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object t = new Object[]
            thisTest.Verify("t", t);
        }

        [Test]
        [Category("Replication")]
        public void T66_Defect_1467198_Inline_Condition_With_Jagged_Array()
        {
            String code =
@"a = { 1, 2};
            string err = "1467198 - Sprint24: rev 3237: Design Issue with Replication on jagged arrays in inline condition";
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);
            thisTest.Verify("x", new Object[] { new Object[] { 0, 1 }, 0 });
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c2", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_2()
        {
            String code =
@"class Point
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("xs", new Object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_3()
        {
            String code =
@"class MyPoint 
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("p2", -20.0);
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_4()
        {
            String code =
@"class MyPoint 
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("p2", new Object[] { 0.0, 1.0, 2.0 });
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_5()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("test", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_6()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("test", new Object[] { new Object[] { 1, 2 }, new Object[] { 2, 3 } });
            thisTest.Verify("test2", new Object[] { 1, 2 });
            thisTest.Verify("test3", new Object[] { 2, 3 });
            thisTest.Verify("test4", 1);
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_8()
        {
            String code =
@"class A
            string error = "1467264 - Sprint25: rev 3548 : over indexing should yield null value";
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code, error);
            Object n1 = null;
            thisTest.Verify("test", new Object[] { new Object[] { 1, 2 }, new Object[] { 2, 3 } });
            thisTest.Verify("test2", new Object[] { 1, 2 });
            thisTest.Verify("test3", new Object[] { 2, 3 });
            thisTest.Verify("test4", n1);
        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_9()
        {
            String code =
@"class A
            string error = "";//1467265 - Sprint25:rev 3548 : accessing array members using bracket should be allowed";
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("test3", 1);

        }

        [Test]
        [Category("Replication")]
        public void T67_Defect_1460965_Replication_On_Dot_Operator_10()
        {
            String code =
@"class A
            string error = "";// "1467266 - Sprint25: rev 3549 : Accessing array members is not giving the expected result";
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code, error);
            Object n1 = null;
            thisTest.Verify("test3", 1);
            thisTest.Verify("test2", new Object[] { 1, 2 });
        }

        [Test]
        public void T67_Defect_1460965_ExpressionInParenthesis01()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("x1", 2);
            thisTest.Verify("x2", 3);
            thisTest.Verify("x3", 4);
        }

        [Test]
        public void T67_Defect_1460965_ExpressionInParenthesis02()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t1", 4);
            thisTest.Verify("t2", 4);
            thisTest.Verify("t3", 4);
        }

        [Test]
        public void T67_Defect_1460965_ExpressionInParenthesis03()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t2", 4);
            thisTest.Verify("t3", 4);
        }

        [Test]
        [Category("Replication")]
        public void T68_Defect_1460965_Replication_On_Dot_Operator_7()
        {
            String code =
@"class A 
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467241 - Sprint25: rev 3420 : Property assignments using replication is not working");
            thisTest.Verify("test", new Object[] { 5, 5 });

        }

        [Test]
        [Category("Replication")]
        public void ArrayConvertTest()
        {
            String code =
@"def foo : int (i : double)
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467241 - Sprint25: rev 3420 : Property assignments using replication is not working");
            thisTest.Verify("b", new Object[] { 2, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T68_Defect_1460965_Replication_On_Dot_Operator_8()
        {
            String code =
@"class A 
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();

            String errmsg = "1467272 - Sprint25: rev 3603: Replication on dot operators not working for jagged arrays";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 1, new Object[] { 2, new Object[] { 0, 1 } } });
        }

        [Test]
        [Category("Replication")]
        public void T68_Defect_1460965_Replication_On_Dot_Operator_9()
        {
            String code =
@"class A 
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467272 - Sprint25: rev 3603: Replication on dot operators not working for jagged arrays";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 5, new Object[] { 5, new Object[] { 5, 5 } } });
        }

        [Test]
        [Category("Replication")]
        public void T69_Replication_Across_Language_Blocks()
        {
            String code =
@"def foo ( p : double)
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("x", new Object[] { 1.0, 2.0 });
        }

        [Test]
        [Category("Replication")]
        public void T70_Defect_1467266()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test3", new Object[] { 1, 2 });
            thisTest.Verify("test2", new Object[] { new Object[] { 1, 2 }, new Object[] { 1, 2 } });
        }

        [Test]
        [Category("Replication")]
        public void T71_Defect_1467209()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);

            thisTest.Verify("test", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T71_Defect_1467209_2()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467265 - Sprint25:rev 3548 : accessing array members/ function calls  using bracket should be allowed";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test2", new Object[] { 1, 2 });
            thisTest.Verify("test3", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T71_Defect_1467209_3()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467265 - Sprint25:rev 3548 : accessing array members/ function calls  using bracket should be allowed";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a1", new Object[] { new Object[] { new Object[] { 1, 2 }, new Object[] { 1, 2 } }, new Object[] { new Object[] { 2, 3 }, new Object[] { 2, 3 } } });

        }

        [Test]
        [Category("Replication")]
        public void T71_Defect_1467209_4()
        {
            String code =
@"class A
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467265 - Sprint25:rev 3548 : accessing array members/ function calls  using bracket should be allowed";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a1", new Object[] { new Object[] { new Object[] { 2, 3 }, new Object[] { 2, 3 } }, new Object[] { new Object[] { 3, 4 }, new Object[] { 3, 4 } } });
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169()
        {
            String code =
@"a = { 1, 2 } ;
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, 1 });
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 0, 1 }, new Object[] { 0, 1 } });
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 1, 2 }, new Object[] { 1, 2 } });
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 4, 6 });
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 4);
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";// "1467284 - Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 4);
        }

        [Test]
        [Category("Replication")]
        public void T72_Defect_1467169_7()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467285 - Sprint25: rev 3711: left assignment using replication on array indices is not working";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("x", new Object[] { n1, 2, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T73_Defect_1467069()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { 10, 2, 2, 2, 14, 15, n1, n1, n1, n1, 2 });
        }
        [Test, Ignore]
        [Category("Replication")]
        public void T73_Defect_1467069_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467070 - Sprint 23 - rev 2636 - 328558 Replication must be disabled in imperative scope";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { 10, 11, 12, 13, 14, 15 });
        }

        [Test]
        [Category("Replication")]
        public void T73_Defect_1467069_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467292 - rev 3746 - REGRESSION :  replication on jagged array is giving unexpected output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { 11, 12, new Object[] { 3, 3 }, null, new Object[] { 2, 2 } });
        }

        [Test]
        [Category("Replication")]
        public void T73_Defect_1467069_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y1", new Object[] { 11, 3, 3, 3, 15, 16, null, null, 1, 1, 1 });
            thisTest.Verify("y2", new Object[] { 10, 2, 2, 2, 14, 15, null, null, 0, 0, 0 });
        }
        [Test, Ignore]
        [Category("Replication")]
        public void T74_Defect_1463465()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467070 - Sprint 23 - rev 2636 - 328558 Replication must be disabled in imperative scope";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("c", n1);

        }

        [Test]
        [Category("Replication")]
        public void T74_Defect_1463465_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c", new Object[] { 2, 2, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467282 - Replication guides not working in constructor of class";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("x", new Object[] { new Object[] { 5, 6 }, new Object[] { 6, 7 } });
            thisTest.Verify("y", new Object[] { 5, 7 });
        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467315 rev 3830 : System.NotImplementedException : only <1> and <2> are supported as replication guides";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("x", new Object[] { new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } }, new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } } });
            thisTest.Verify("y", new Object[] { new Object[] { 5, 7 }, new Object[] { 5, 7 } });
        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467315 rev 3830 : System.NotImplementedException : only <1> and <2> are supported as replication guides";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("test", new Object[] { new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } }, new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } } });
        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467298 rev 4245 :  replication guides with partial array indexing is not giving the expected output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);

            thisTest.Verify("x", new Object[] { new Object[] { 5, 6 }, new Object[] { 7, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { new Object[] { 0, 2, 4 }, new Object[] { 3, 5, 7 } }, new Object[] { new Object[] { 3, 5, 7 }, new Object[] { 6, 8, 10 } } });
            thisTest.Verify("test2", new Object[] { new Object[] { new Object[] { 0, 2, 4 }, new Object[] { 3, 5, 7 } }, new Object[] { new Object[] { 3, 5, 7 }, new Object[] { 6, 8, 10 } } });

        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 5, 6 }, new Object[] { 6, 7 } });

        }

        [Test]
        [Category("Replication")]
        public void T75_Defect_1467282_7()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467398 Rev 4319 : Replication guides applied directly on collections not giving expected output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
            thisTest.Verify("test1", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
            thisTest.Verify("test2", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T76_Defect_1467254()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a1", 11);
        }

        [Test]
        [Category("Replication")]
        public void T76_Defect_1467254_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a1", 8);
        }

        [Test]
        [Category("Replication")]
        public void T77_Defect_1467081()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("y", new Object[] { 0, 1 });
            thisTest.Verify("z", new Object[] { 1, 2, n1 });
            thisTest.Verify("z2", new Object[] { 2, 1, 0 });
        }

        [Test]
        [Category("Replication")]
        public void T77_Defect_1467081_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467284 Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("y", new Object[] { 0, 4 });

        }

        [Test]
        [Category("Replication")]
        public void T77_Defect_1467081_3()
        {
            String code =
                @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467318 - Cannot return an array from a function whose return type is var with undefined rank (-2)";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("x1", new Object[] { 0, 1 });
            thisTest.Verify("x2", new Object[] { 1, 2, n1 });
            thisTest.Verify("x3", new Object[] { 2, 1, 0 });
        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467125 Sprint 24 - Rev 2877 replication does not work with higher ranks , throws error Method resolution failure";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { new Object[] { 10, 20 } });

        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { new Object[] { 10, 20 } });
        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { new Object[] { 10, 20 } });
        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { new Object[] { 10 } });
        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { 3, 8 });
        }

        [Test]
        [Category("Replication")]
        public void T78_Defect_1467125_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467125 Sprint 24 - Rev 2877 replication does not work with higher ranks , throws error Method resolution failure";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("rab", new Object[] { new Object[] { 3, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T79_Defect_1467096()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { false, false });
        }

        [Test]
        [Category("Replication")]
        public void T79_Defect_1467096_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { -1, -2 });
        }

        [Test]
        [Category("Replication")]
        public void T79_Defect_1467096_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T79_Defect_1467096_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { -1, -2 });
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", -1);
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { -1, -2, -3 });
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", -1);
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", -1);
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", 1);
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_7()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { -1, -1 });
        }

        [Test]
        [Category("Replication")]
        public void T80_Defect_1467297_8()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467297 rev 3769 : parser issue over negating a property of an instance";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, -1 });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467298()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467298 rev 3769 : replication guides with partial array indexing is not supported by parser";

            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { 5, 7 });
            thisTest.Verify("c2", new Object[] { new Object[] { 4, 5, 6 }, new Object[] { 5, 6, 7 }, new Object[] { 6, 7, 8 } });
            thisTest.Verify("c3", new Object[] { new Object[] { 5, 6 }, new Object[] { 6, 7 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4, 5, 6 }, new Object[] { 5, 6, 7 }, new Object[] { 6, 7, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4, 5, 6 }, new Object[] { 5, 6, 7 }, new Object[] { 6, 7, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4, 5, 6 }, new Object[] { 5, 6, 7 }, new Object[] { 6, 7, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4.0, 5.0, 6.0 }, new Object[] { 5.0, 6.0, 7.0 }, new Object[] { 6.0, 7.0, 8.0 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4.0, 5.0, 6.0 }, new Object[] { 5.0, 6.0, 7.0 }, new Object[] { 6.0, 7.0, 8.0 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4.0, 5.0, 6.0 }, new Object[] { 5.0, 6.0, 7.0 }, new Object[] { 6.0, 7.0, 8.0 } });
        }

        [Test]
        [Category("Replication")]
        public void T81_Defect_1467299_7()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";// "DNL-1467299 rev 3769 : System.NotImplemented exc eption when replication guides are used to call functions with no type specified in arguments";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c1", new Object[] { new Object[] { 4, 5, 6 }, new Object[] { 5, 6, 7 }, new Object[] { 6, 7, 8 } });
        }

        [Test]
        [Category("Replication")]
        public void T82_Defect_1467244()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467224 Sprint25: rev 3352: method dispatch over heterogeneous array is not correct";

            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("v1", 100);
            thisTest.Verify("v2", n1);
            thisTest.Verify("v3", new Object[] { 100, 100, n1 });
        }

        [Test]
        [Category("Replication")]
        public void T82_Defect_1467244_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467224 Sprint25: rev 3352: method dispatch over heterogeneous array is not correct";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("v3", n1);
        }

        [Test]
        [Category("Replication")]
        public void T82_Defect_1467244_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467224 Sprint25: rev 3352: method dispatch over heterogeneous array is not correct";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);

            thisTest.Verify("v3", new Object[] { 100, 100 });
        }

        [Test]
        [Category("Replication")]
        public void T83_Defect_1467253()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { new Object[] { 2, 2 }, new Object[] { 4, 4 } }, new Object[] { new Object[] { 2, 2 }, new Object[] { 4, 4 } } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//1467313 - rev 3791: replication on arguments that expect one dimensional array using higher ranks is giving ambiguous output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
            thisTest.Verify("test2", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
            thisTest.Verify("test2", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_7()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 4 }, new Object[] { 2, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T84_Defect_1467313_8()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
        }

        [Test]
        [Category("Replication")]
        public void T85_Defect_1467076()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467076 - Function overloading in derived class causes Internal error: Recursion past base case {158ECB5E-2139-4DD7-9470-F64A42CE0D6D} ";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("val1", null);
        }

        [Test]
        [Category("Replication")]
        public void T85_Defect_1467076_a()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467076 - Function overloading in derived class causes Internal error: Recursion past base case {158ECB5E-2139-4DD7-9470-F64A42CE0D6D} ";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("val1", 1);
        }

        [Test]
        [Category("Replication")]
        public void T85_Defect_1467076_2()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467076 - Function overloading in derived class causes Internal error: Recursion past base case {158ECB5E-2139-4DD7-9470-F64A42CE0D6D} ";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("val1", null);
            thisTest.Verify("val2", null);
        }

        [Test]
        [Category("Replication")]
        public void T85_Defect_1467076_2b()
        {
            String code =
                    @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467076 - Function overloading in derived class causes Internal error: Recursion past base case {158ECB5E-2139-4DD7-9470-F64A42CE0D6D} ";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("val1", 1);
            thisTest.Verify("val2", 2);
        }

        [Test]
        [Category("Replication")]
        public void T86_Defect_1467285()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("x", new Object[] { n1, 2, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T86_Defect_1467285_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467284 Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("x", new Object[] { n1, new Object[] { n1, 2 }, new Object[] { n1, n1, 2 } });
        }

        [Test]
        [Category("Replication")]
        public void T86_Defect_1467285_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467284 Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { 2 } }, new Object[] { n1, new Object[] { n1, 2 } } });
        }

        [Test]
        [Category("Replication")]
        public void T86_Defect_1467285_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467284 Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);

            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { 2, 0 }, new Object[] { 0, 0 } }, new Object[] { new Object[] { 0, 0 }, new Object[] { 0, 2 } } });
        }

        [Test]
        [Category("Replication")]
        public void T87_Defect_1467284()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467284 Sprint25: rev 3705: replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { 2, 1.5 }, new Object[] { 5.1, 0.0 } }, new Object[] { new Object[] { n1, n1 }, new Object[] { true, 2 } } });
            thisTest.Verify("test", new Object[] { 2, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T88_Defect_1467296()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T88_Defect_1467296_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 1, 2 });
        }

        [Test]
        [Category("Replication")]
        public void T88_Defect_1467296_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { new Object[] { 1, 2 }, new Object[] { -1, -2 } });
        }

        [Test]
        [Category("Replication")]
        public void T88_Defect_1467296_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467296 rev 3769 : Replication over array indices not working for class properties";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { new Object[] { 1, 2 }, new Object[] { 1, 2 } });
        }

        [Test]
        [Category("Replication")]
        public void T89_Defect_1467328()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";

            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("x", new Object[] { new Object[] { 4, 5 }, new Object[] { 5, 6 } });
        }

        [Test]
        [Category("Replication")]
        public void T90_Defect_1467285()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467285 Sprint25: rev 3711: left assignment using replication on array indices should follow zipped collection rule";

            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a", new Object[] { 3, 4, 3, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T91_Defect_1467285_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467285 Sprint25: rev 3711: left assignment using replication on array indices should follow zipped collection rule";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a", new Object[] { 3, 4, 3, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T91_Defect_1467285_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 3, 3, 3, 4 });
            thisTest.Verify("c", new Object[] { 1, 2, 3, 4, n1, 1, 2 });
            thisTest.Verify("d", new Object[] { 3, 2, 3, 4 });
        }

        [Test]
        [Category("Replication")]
        public void T91_Defect_1467285_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b1", new Object[] { 3, 3, 3, 4 });
            thisTest.Verify("c1", new Object[] { 1, 2, 3, 4, n1, 1, 2 });
            thisTest.Verify("d1", new Object[] { 3, 2, 3, 4 });
        }

        [Test] 
        [Category("Replication")]
        public void T91_Defect_1467285_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            Object n1 = null;
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("b", new Object[] { 7, 8, 7, 8 });
        }

        [Test]
        [Category("Replication")]
        public void T92_add()
        {
            String code =
@"
            string errmsg = "DNL-1467292 rev 3746 - REGRESSION :  replication on jagged array is giving unexpected output";
            thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("c", new object[] { new object[] { 6, 7 }, new object[] { 9, 10 } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467315 rev 3830 : System.NotImplementedException : only <1> and <2> are supported as replication guides";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } }, new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } }, new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } }, new Object[] { new Object[] { new Object[] { 5, 6 }, new Object[] { 5, 6 } }, new Object[] { new Object[] { 6, 7 }, new Object[] { 6, 7 } } } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { 6, 7 } }, new Object[] { new Object[] { 6, 7 } } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            thisTest.Verify("y", new Object[] { new Object[] { new Object[] { 10, 12 } }, new Object[] { new Object[] { 10, 12 } } });
        }

        [Test]
        [Category("Replication")]
        public void T93_Defect_1467315_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            // verification 
            // expected : y = {{{{{{10,11}},{{10,11}}},{{{11,12}},{{11,12}}}}},{{{{{10,11}},{{10,11}}},{{{11,12}},{{11,12}}}}}}
            thisTest.Verify("t1", new Object[] { 10, 11 });
        }

        [Test]
        [Category("Replication")]
        public void T94_Defect_1467265()
        {
            String code =
@"nums = 10;
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("n", new Object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        }

        [Test]
        [Category("Replication")]
        public void T94_Defect_1467265_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 1);
        }

        [Test]
        public void T94_Defect_1467265_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 1);
        }

        [Test]
        public void T94_Defect_1467265_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 1, 1 });
        }

        [Test]
        public void T94_Defect_1467265_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 1, 1 });
        }

        [Test]
        public void T94_Defect_1467265_6()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { 1, 1 });
            thisTest.Verify("test2", new Object[] { 1, 1 });
        }

        [Test]
        public void T95_Defect_1467398_Replication_Guides_On_Collection()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467398 Rev 4319 : Replication guides applied directly on collections not giving expected output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } });
            thisTest.Verify("test1", new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } });
            thisTest.Verify("test2", new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } });
        }

        [Test]
        public void T95_Defect_1467398_Replication_Guides_On_Collection_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { new Object[] { new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } }, new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } } }, new Object[] { new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } }, new Object[] { new Object[] { 0, 0 }, new Object[] { 1, 1 } } } };
            thisTest.Verify("test", v1);
            thisTest.Verify("test1", v1);
            thisTest.Verify("test2", v1);
        }

        [Test]
        public void T95_Defect_1467398_Replication_Guides_On_Collection_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";//DNL-1467398 Rev 4319 : Replication guides applied directly on collections not giving expected output";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
            thisTest.Verify("test1", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
            thisTest.Verify("test2", new Object[] { new Object[] { 2, 3 }, new Object[] { 3, 4 } });
        }

        [Test]
        public void T95_Defect_1467398_Replication_Guides_On_Collection_4()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 2);
        }

        [Test]
        public void T95_Defect_1467398_Replication_Guides_On_Collection_5()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("test", 2);
        }

        [Test]
        public void T96_Defect_1467192_Replication_Inline_Condition()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { false, false };
            thisTest.Verify("d2", new Object[] { v1, v1 });
            thisTest.Verify("f2", v1);

        }

        [Test]
        public void T96_Defect_1467192_Replication_Inline_Condition_2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { false, false };
            thisTest.Verify("d2", new Object[] { v1, v1 });
        }

        [Test]
        public void T96_Defect_1467192_Replication_Inline_Condition_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { false, true };
            thisTest.Verify("test1", new Object[] { new Object[] { 0, 0 }, 1 });
            thisTest.Verify("test2", v1);
        }

        [Test]
        public void T97_Defect_1467408_Replication_On_Class_Property_Assignment()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467408 Rev 4443  :Replication on class property assignment is not working";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { false, false };
            thisTest.Verify("test1", new Object[] { 2, 3 });
        }

        [Test]
        public void T98_replication_1467453()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "1467453 - throws error index out of range  for valid code ";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object[] v1 = new Object[] { false, false };
            thisTest.Verify("a", 3);
            thisTest.Verify("b", new Object[] { 3, 1 });
            thisTest.Verify("d", new Object[] { 11, new object[] { 4, 1 } });
            thisTest.Verify("c", new Object[] { new object[] { 14 }, new object[] { 17 } });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_01()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);

            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_02()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a", new Object[] { 1, 3 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_03()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("a", new Object[] { 1, 3 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_04()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_05()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 3 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_06()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 2, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_07()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_08()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_09()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_10()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_11()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_12()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_13()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_14()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_15()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_16()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "DNL-1467375 Sprint 27 - Rev 4127 - in the atatched example the result is expected to be zipped .";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("a", new Object[] { 1, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_17()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("d1", new Object[] { 1 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_18()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("y", new Object[] { 0 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_19()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("b", 2);
        }

        [Test]
        public void T100_Replication_On_Class_Instance_20()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            Object n1 = null;
            thisTest.Verify("res", 1);
        }

        [Test]
        public void T100_Replication_On_Class_Instance_21()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 1, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_22()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 2, 3 });
        }


        [Test]
        public void T100_Replication_On_Class_Instance_23()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 2, 3 });
        }


        [Test]
        public void T100_Replication_On_Class_Instance_24()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 2, 3 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_25()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 2, 2 });
        }

        [Test]
        public void T100_Replication_On_Class_Instance_26()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            String errmsg = "";
            ExecutionMirror mirror = thisTest.VerifyRunScriptSource(code, errmsg);
            thisTest.Verify("res", new Object[] { 2 });
        }
    }

}