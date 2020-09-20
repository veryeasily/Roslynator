﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// <auto-generated>

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class AnalyzerOptionsAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(AnalyzerOptions.UseImplicitlyTypedArrayWhenTypeIsObvious, AnalyzerOptions.UseImplicitlyTypedArray, AnalyzerOptions.RemoveAccessibilityModifiers, AnalyzerOptions.RemoveEmptyLineBetweenClosingBraceAndSwitchSection, AnalyzerOptions.DoNotRenamePrivateStaticReadOnlyFieldToCamelCaseWithUnderscore, AnalyzerOptions.RemoveArgumentListFromObjectCreation, AnalyzerOptions.RemoveParenthesesFromConditionOfConditionalExpressionWhenExpressionIsSingleToken, AnalyzerOptions.ConvertBitwiseOperationToHasFlagCall, AnalyzerOptions.SimplifyConditionalExpressionWhenItIncludesNegationOfCondition, AnalyzerOptions.ConvertMethodGroupToAnonymousFunction, AnalyzerOptions.DoNotUseElementAccessWhenExpressionIsInvocation, AnalyzerOptions.UseIsNullPatternInsteadOfInequalityOperator, AnalyzerOptions.UseComparisonInsteadOfIsNullPattern);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
        }
    }
}