﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data
Imports System.Data.Odbc

''' <summary>
''' Summary description for DataHelper
''' </summary>
Public Class DataHelper
	Private Shared Function GetConnection() As OdbcConnection
		Try
			Dim connection As New OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=|DataDirectory|\nwind.mdb;Uid=;Pwd=;")
			connection.Open()
			Return connection
		Catch ex As Exception
			Throw New Exception(String.Format("Connection failed!: {0}", ex.Message), ex)
		End Try
	End Function

	Public Shared Function ExecuteCommand(ByVal commandFormat As String, ParamArray ByVal commandParameters() As Object) As Object
		Try
			Dim commandText As String = String.Format(commandFormat, commandParameters)
			Using connection As OdbcConnection = GetConnection()
				Dim command As New OdbcCommand(commandText, connection)
				Dim result As Object = command.ExecuteScalar()
				connection.Close()
				Return result
			End Using
		Catch ex As Exception
			Throw New Exception(String.Format("Command executing failed!: {0}", ex.Message), ex)
		End Try
	End Function

	Public Shared Function ProcessSelectCommand(ByVal commandFormat As String, ParamArray ByVal commandParameters() As Object) As DataTable
		Try
			Dim commandText As String = String.Format(commandFormat, commandParameters)
			Using connection As OdbcConnection = GetConnection()
				Dim adapter As New OdbcDataAdapter(commandText, connection)
				Dim table As New DataTable()
				adapter.Fill(table)
				Return table
			End Using
		Catch ex As Exception
			Throw New Exception(String.Format("Data selecting failed!: {0}", ex.Message), ex)
		End Try
	End Function
End Class
