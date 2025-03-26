#pragma once
#include "NewProject.h"

namespace Forms_and_OpenGL {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// —водка дл€ EngineForms
	/// </summary>
	public ref class EngineForms : public System::Windows::Forms::Form
	{
	public:
		EngineForms(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

		//получить дескриптор окна
		IntPtr getPanelHandle()
		{
			return panel1->Handle;
		}
		//получить панель окна
		Panel^ getPanel()
		{
			return panel1;
		}

	protected:
		/// <summary>
		/// ќсвободить все используемые ресурсы.
		/// </summary>
		~EngineForms()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Panel^  panel1;
	private: System::Windows::Forms::MenuStrip^  menuStrip1;
	private: System::Windows::Forms::ToolStripMenuItem^  fileToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  newProjectToolStripMenuItem;
	protected: 

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
			this->panel1 = (gcnew System::Windows::Forms::Panel());
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->fileToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->newProjectToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->menuStrip1->SuspendLayout();
			this->SuspendLayout();
			// 
			// panel1
			// 
			this->panel1->BackColor = System::Drawing::SystemColors::AppWorkspace;
			this->panel1->Location = System::Drawing::Point(12, 60);
			this->panel1->Name = L"panel1";
			this->panel1->Size = System::Drawing::Size(684, 440);
			this->panel1->TabIndex = 0;
			// 
			// menuStrip1
			// 
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->fileToolStripMenuItem });
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(960, 24);
			this->menuStrip1->TabIndex = 1;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this->fileToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->newProjectToolStripMenuItem });
			this->fileToolStripMenuItem->Name = L"fileToolStripMenuItem";
			this->fileToolStripMenuItem->Size = System::Drawing::Size(37, 20);
			this->fileToolStripMenuItem->Text = L"File";
			// 
			// newProjectToolStripMenuItem
			// 
			this->newProjectToolStripMenuItem->Name = L"newProjectToolStripMenuItem";
			this->newProjectToolStripMenuItem->Size = System::Drawing::Size(138, 22);
			this->newProjectToolStripMenuItem->Text = L"New Project";
			this->newProjectToolStripMenuItem->Click += gcnew System::EventHandler(this, &EngineForms::newProjectToolStripMenuItem_Click);
			// 
			// EngineForms
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(960, 539);
			this->Controls->Add(this->panel1);
			this->Controls->Add(this->menuStrip1);
			this->MainMenuStrip = this->menuStrip1;
			this->Name = L"EngineForms";
			this->Text = L"EngineForms";
			this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &EngineForms::EngineForms_FormClosing);
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void newProjectToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e)
		{
			Forms_and_OpenGL::NewProject ^new_project = gcnew Forms_and_OpenGL::NewProject;
			new_project->ShowDialog();
		}
	private: System::Void EngineForms_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) 
		{
		
		}
};
}
