#include <iostream>
#include <string> 
#include <fstream>
#include <vector>

using namespace std;
vector <int> stack;
int PC = 0, TC = 0, PS;

void info()
{
	cout << "\nPC = " << ++PC;
	cout << "\nTC = " << (((++TC) % 2 == 0) ? "2" : "1");
	PS = (stack.back() >= 0) ? 0 : 1;

	cout << "\nPS = " << PS << endl;
	cout << "________________________";
	cout << endl;
}

void output(int b)
{
	int count = 0;
	for (int i = 17; i >= 0; i--)
	{
		if (count < 4) {
			putc((b & (1 << i)) ? '1' : '0', stdout);
			count++;
		}
		else {
			cout << " ";
			putc((b & (1 << i)) ? '1' : '0', stdout);
			count = 1;
		}
	}
	cout << endl;
}
void move(int a) {

	cout << "Read Command: Move " << " " << a << endl;
	for (int i = stack.size(); i > 0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i-1]);
	}
	
	info();

	stack.push_back(a);
	cout << "Execute Command: Move "<< " " << a << endl;
	for (int i = stack.size(); i > 0; i--)
		{
			cout << "stack["<<i<<"]" << " = "; output(stack[i-1]);
		}
	--PC;
	info();
}
void del() {

	cout << "Read Command: Delete  " << endl;
	for (int i = stack.size(); i > 0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i - 1]);
	}

	info();

	stack.pop_back();
	cout << "Execute Command: Delete  " << " " << endl;
	for (int i = stack.size(); i > 0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i - 1]);
	}
	--PC;
	info();
}
void move_l(int a) {
	cout << "Read Command: Move left " << " " << a << endl;
	for (int i = stack.size() ; i >0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i-1]);
	}

	info();
		stack.back() = stack.back() << a;
		cout << "Execute Command: Move left " << " " << a << endl;
		for (int i = stack.size() ; i > 0; i--)
		{
			cout << "stack[" << i << "]" << " = "; output(stack[i-1]);
		}
	--PC;
	info();
}

void move_r( int a) {
	cout << "Read Command: Move right " <<  " " << a << endl;
	for (int i = stack.size() ; i >0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i-1]);
	}
	info();
	stack.back() = stack.back() >> a;
	cout << "Execute Command: Move right " << " " << a << endl;
   for (int i = stack.size(); i > 0; i--)
	{
		cout << "stack[" << i << "]" << " = "; output(stack[i-1]);
	}
	--PC;
	info();
}

int main()
{
	stack.push_back(0);
	string comm;
	int a;
	ifstream file("C:/Users/Hewlett-Packard/Desktop/commands.txt");
	while (file >> comm >> a)
	{
		if (comm == "mov")
		{
			move(a);
		}

		else if (comm == "mov_l")
		{
			move_l(a);
		}
		else if (comm == "mov_r")
		{
			move_r(a);
		}
		else
		{
			del();
		}



	}

	return 0;
}
