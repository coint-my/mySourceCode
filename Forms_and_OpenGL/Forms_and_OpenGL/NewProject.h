#pragma once
#include "Property.h"
#include <tchar.h>

namespace Forms_and_OpenGL {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// —водка дл€ NewProject
	/// </summary>
	public ref class NewProject : public System::Windows::Forms::Form
	{
	public:
		NewProject(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// ќсвободить все используемые ресурсы.
		/// </summary>
		~NewProject()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^  label1;
	protected: 
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::NumericUpDown^  numericWidth;
	private: System::Windows::Forms::NumericUpDown^  numericHeight;
	private: System::Windows::Forms::Button^  buttonCancel;
	private: System::Windows::Forms::Button^  buttonOk;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::TextBox^  textBoxName;

	private:
		/// <summary>
		/// “ребуетс€ переменна€ конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// ќб€зательный метод дл€ поддержки конструктора - не измен€йте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->numericWidth = (gcnew System::Windows::Forms::NumericUpDown());
			this->numericHeight = (gcnew System::Windows::Forms::NumericUpDown());
			this->buttonCancel = (gcnew System::Windows::Forms::Button());
			this->buttonOk = (gcnew System::Windows::Forms::Button());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->textBoxName = (gcnew System::Windows::Forms::TextBox());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numericWidth))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numericHeight))->BeginInit();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(25, 51);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(89, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"¬ведите ширину";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(25, 86);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(88, 13);
			this->label2->TabIndex = 1;
			this->label2->Text = L"¬ведите высоту";
			// 
			// numericWidth
			// 
			this->numericWidth->Increment = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			this->numericWidth->Location = System::Drawing::Point(166, 49);
			this->numericWidth->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) {2000, 0, 0, 0});
			this->numericWidth->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			this->numericWidth->Name = L"numericWidth";
			this->numericWidth->Size = System::Drawing::Size(78, 20);
			this->numericWidth->TabIndex = 2;
			this->numericWidth->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			// 
			// numericHeight
			// 
			this->numericHeight->Increment = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			this->numericHeight->Location = System::Drawing::Point(166, 84);
			this->numericHeight->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) {1500, 0, 0, 0});
			this->numericHeight->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			this->numericHeight->Name = L"numericHeight";
			this->numericHeight->Size = System::Drawing::Size(78, 20);
			this->numericHeight->TabIndex = 3;
			this->numericHeight->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) {10, 0, 0, 0});
			// 
			// buttonCancel
			// 
			this->buttonCancel->Location = System::Drawing::Point(169, 124);
			this->buttonCancel->Name = L"buttonCancel";
			this->buttonCancel->Size = System::Drawing::Size(75, 23);
			this->buttonCancel->TabIndex = 4;
			this->buttonCancel->Text = L"cancel";
			this->buttonCancel->UseVisualStyleBackColor = true;
			this->buttonCancel->Click += gcnew System::EventHandler(this, &NewProject::buttonCancel_Click);
			// 
			// buttonOk
			// 
			this->buttonOk->Enabled = false;
			this->buttonOk->Location = System::Drawing::Point(88, 124);
			this->buttonOk->Name = L"buttonOk";
			this->buttonOk->Size = System::Drawing::Size(75, 23);
			this->buttonOk->TabIndex = 5;
			this->buttonOk->Text = L"ok";
			this->buttonOk->UseVisualStyleBackColor = true;
			this->buttonOk->Click += gcnew System::EventHandler(this, &NewProject::buttonOk_Click);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(25, 15);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(72, 13);
			this->label3->TabIndex = 6;
			this->label3->Text = L"¬ведите им€";
			// 
			// textBoxName
			// 
			this->textBoxName->Location = System::Drawing::Point(103, 12);
			this->textBoxName->MaxLength = 20;
			this->textBoxName->Name = L"textBoxName";
			this->textBoxName->Size = System::Drawing::Size(141, 20);
			this->textBoxName->TabIndex = 7;
			this->textBoxName->TextChanged += gcnew System::EventHandler(this, &NewProject::textBoxName_TextChanged);
			// 
			// NewProject
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(256, 159);
			this->Controls->Add(this->textBoxName);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->buttonOk);
			this->Controls->Add(this->buttonCancel);
			this->Controls->Add(this->numericHeight);
			this->Controls->Add(this->numericWidth);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = L"NewProject";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"NewProject";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numericWidth))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numericHeight))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
private: System::Void buttonCancel_Click(System::Object^  sender, System::EventArgs^  e) 
		{
			this->Close();
		}
private: System::Void buttonOk_Click(System::Object^  sender, System::EventArgs^  e)
		{
			W_WIDTH = (int)this->numericWidth->Value;
			W_HEIGHT = (int)this->numericHeight->Value;
			for (int i = 0; i < textBoxName->Text->Length; i++)
			{
				NameProject[i] = Convert::ToChar(textBoxName->Text[i]);
			}

			this->Close();
		}
private: System::Void textBoxName_TextChanged(System::Object^  sender, System::EventArgs^  e) 
		{
			if(textBoxName->Text->Length > 3)
				buttonOk->Enabled = true;
			else
				buttonOk->Enabled = false;
		}
};
}
