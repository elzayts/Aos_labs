#include <iostream>
#include <cstdlib>
#include <chrono>
#include <iomanip>
 
using namespace std;

template <typename T1>
auto func_empty(T1 a, T1 b, T1 c, T1 d, T1 e, T1 f)
{
	int iter = 1000000;
	std::chrono::time_point<std::chrono::high_resolution_clock> start1, end1;

	start1 = std::chrono::high_resolution_clock::now();
	for (int i = 0; i <= iter; i++)
	{
		a = c;
		b = a;
		c = b;
		f = c;
		e = d;
		a = e;
		f = a;
		c = b;
		d = c;
		e = i;

	}
	end1 = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> empty_for = end1 - start1;

	return empty_for;
}

//-------------------------------------------------------------------------------- 

template <typename T1>
double func_plus(T1 a, T1 b, T1 c, T1 d, T1 e, T1 f)
{
	int iter = 1000000;
	std::chrono::time_point<std::chrono::high_resolution_clock> start, end, start1, end1;

	auto empty = func_empty(a, b, c, d, e, f); //empty "for" duration


	start = std::chrono::high_resolution_clock::now();

	for (int i = 0; i <= iter; i++)
	{
		a = c + d;
		b = a + e;
		c = b + a;
		f = c + b;
		e = d + c;
		a = e + d;
		f = a + e;
		c = b + d;
		d = c + b;
		e = i + f;

	}

	end = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> elapsed_seconds = end - start;

	return iter /abs((elapsed_seconds.count() - empty.count()));
}

//-----------------------------------------------------------------------

template <typename T1>
double func_minus(T1 a, T1 b, T1 c, T1 d, T1 e, T1 f)
{
	int iter = 1000000;
	std::chrono::time_point<std::chrono::high_resolution_clock> start, end, start1, end1;

	auto empty = func_empty(a, b, c, d, e, f); //empty "for" duration


	start = std::chrono::high_resolution_clock::now();

	for (int i = 0; i <= iter; i++)
	{
		a = c - d;
		b = a - e;
		c = b - a;
		f = c - b;
		e = d - c;
		a = e - d;
		f = a - e;
		c = b - d;
		d = c - b;
		e = i - f;

	}

	end = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> elapsed_seconds = end - start;

	return iter / abs((elapsed_seconds.count() - empty.count()));
}

//-----------------------------------------------------------------------

template <typename T1>
double func_multip(T1 a, T1 b, T1 c, T1 d, T1 e, T1 f)
{
	int iter = 1000000;
	std::chrono::time_point<std::chrono::high_resolution_clock> start, end, start1, end1;

	auto empty = func_empty(a, b, c, d, e, f); //empty "for" duration


	start = std::chrono::high_resolution_clock::now();

	for (int i = 0; i <= iter; i++)
	{
		a = c * d;
		b = a * e;
		c = b * a;
		f = c * b;
		e = d * c;
		a = e * d;
		f = a * e;
		c = b * d;
		d = c * b;
		e = i * f;

	}

end = std::chrono::high_resolution_clock::now();
std::chrono::duration<double> elapsed_seconds = end - start;

return iter / abs((elapsed_seconds.count() - empty.count()));
}

//-----------------------------------------------------------------------

template <typename T1>
double func_divide(T1 a, T1 b, T1 c, T1 d, T1 e, T1 f)
{
	int iter = 1000000;
	std::chrono::time_point<std::chrono::high_resolution_clock> start, end, start1, end1;

	auto empty = func_empty(a, b, c, d, e, f); //empty "for" duration


	start = std::chrono::high_resolution_clock::now();

	for (int i = 0; i <= iter; i++)
	{
		a = c / (++d);
		b = a / (++e);
		c = b / (++a);
		f = c / (++b);
		e = d / (++c);
		a = e / (++d);
		f = a / (++e);
		c = b / (++d);
		d = c / (++b);
		e = i / (++f);

	}

	end = std::chrono::high_resolution_clock::now();
	std::chrono::duration<double> elapsed_seconds = end - start;

	return iter / abs((elapsed_seconds.count() - empty.count()));
}

int main()
{

	int i_a = 3, i_b = 23, i_c = 123, i_d = 345, i_e = 43, i_f = 5;
	long long ll_a = 300, ll_b = 2345, ll_c = 12323, ll_d = 345, ll_e = 43222, ll_f = 536;
	float f_a = 3345.262, f_b = 23.23734, f_c = 123.33475, f_d = 34533.3488, f_e = 4335.2345, f_f = 500.3567;
	double d_a = 323456.357357346, d_b = 23234.2579235, d_c = 123.29751601555, d_d = 345.367, d_e = 43.00001, d_f = 5.987634567898765;
	
	double result[4][4], res_percent[4][4];

	for (int i = 0; i < 5; i++) {
		func_plus(i_a, i_b, i_c, i_d, i_e, i_f);
	}

	result[0][0] = func_plus(i_a, i_b, i_c, i_d, i_e, i_f);
	result[0][1] = func_minus(i_a, i_b, i_c, i_d, i_e, i_f);
	result[0][2] = func_multip(i_a, i_b, i_c, i_d, i_e, i_f);
	result[0][3] = func_divide(i_a, i_b, i_c, i_d, i_e, i_f);


	result[1][0] = func_plus(ll_a, ll_b, ll_c, ll_d, ll_e, ll_f);
	result[1][1] = func_minus(ll_a, ll_b, ll_c, ll_d, ll_e, ll_f);
	result[1][2] = func_multip(ll_a, ll_b, ll_c, ll_d, ll_e, ll_f);
	result[1][3] = func_divide(ll_a, ll_b, ll_c, ll_d, ll_e, ll_f);

	result[2][0] = func_plus(f_a, f_b, f_c, f_d, f_e, f_f);
	result[2][1] = func_minus(f_a, f_b, f_c, f_d, f_e, f_f);
	result[2][2] = func_multip(f_a, f_b, f_c, f_d, f_e, f_f);
	result[2][3] = func_divide(f_a, f_b, f_c, f_d, f_e, f_f);

	result[3][0] = func_plus(d_a, d_b, d_c, d_d, d_e, d_f);
	result[3][1] = func_minus(d_a, d_b, d_c, d_d, d_e, d_f);
	result[3][2] = func_multip(d_a, d_b, d_c, d_d, d_e, d_f);
	result[3][3] = func_divide(d_a, d_b, d_c, d_d, d_e, d_f);

	double max = result[0][0];

	for (int i = 0; i < 4; i++)
	{
		for (int l = 0; l < 4; l++)
		{
			if (result[i][l] >= max)
			{
				max = result[i][l];
			}
		}
	}

	for (int i = 0; i < 4; i++)
	{
		for (int l = 0; l < 4; l++)
		{
			res_percent[i][l] = floor(result[i][l]*100/max + 0.5);
		}
	}

	for (int i = 0; i < 4; i++)
	{
		for (int l = 0; l < 4; l++)
		{
			switch (l)
			{
			case 0: 
			{
				cout << " + ";
				break;
			}
			case 1:
			{
				cout << " - ";
				break;
			}
			case 2: 
			{
				cout << " * ";
				break;
			}
			case 3: 
			{
				cout << " / ";
				break;
			}
			}

			switch (i)
			{
			case 0:
			{
				cout <<setw(10) << left << "int";
				break;
			}
			case 1: 
			{
				cout<<setw(10) << left << "long long ";
				break;
			}
			case 2:
			{
				cout<< setw(10) << left << "float";
				break;
			}
			case 3: 
			{
				cout << setw(10) << left << "double";
				break;
			}
			}
			cout << setw(12) << left << result[l][i] << "  ";
			
			
			for (int j = 0; j <40; j++)
			{
				while (j < floor(res_percent[l][i] / 2.5 + 0.5))
				{
					cout << 'x';
					j++;
					
				}
				if (j == 40) { break; }
				cout << ' ';
			}
			cout << " ";
			cout<<setw(0)<< right << res_percent[l][i] << "%"<<endl;
		}
	}
	
	return 0;

}